using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private ArrowView arrowViewPrefab;
    [SerializeField] private Transform arrowContainer;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private Vector3 boardOrigin = Vector3.zero;
    [SerializeField] private float exitDistanceInCells = 1.25f;

    private readonly Dictionary<int, ArrowView> arrowViewsById = new Dictionary<int, ArrowView>();

    private BoardState boardState;
    private GameController gameController;

    public void Initialize(BoardState initialBoardState, GameController controller)
    {
        boardState = initialBoardState;
        gameController = controller;

        if (arrowViewPrefab == null)
        {
            Debug.LogError("BoardView: ArrowView prefab reference is missing.");
            return;
        }

        if (arrowContainer == null)
        {
            Debug.LogError("BoardView: ArrowContainer reference is missing.");
            return;
        }

        SpawnAllArrows();

        gameController.OnArrowRemovalStarted += HandleArrowRemovalStarted;
        gameController.OnLevelCompleted += HandleLevelCompleted;
    }

    private void OnDestroy()
    {
        if (gameController == null)
        {
            return;
        }

        gameController.OnArrowRemovalStarted -= HandleArrowRemovalStarted;
        gameController.OnLevelCompleted -= HandleLevelCompleted;
    }

    private void SpawnAllArrows()
    {
        foreach (ArrowData arrowData in boardState.GetAllArrows())
        {
            SpawnArrowView(arrowData);
        }
    }

    private void SpawnArrowView(ArrowData arrowData)
    {
        Vector3 worldPosition = GridToWorld(arrowData.Position);

        ArrowView arrowView = Instantiate(
            arrowViewPrefab,
            worldPosition,
            Quaternion.identity,
            arrowContainer
        );

        arrowView.Initialize(arrowData, gameController, this);
        arrowViewsById.Add(arrowData.Id, arrowView);
    }

    private void HandleArrowRemovalStarted(ArrowData arrowData)
    {
        if (!arrowViewsById.TryGetValue(arrowData.Id, out ArrowView arrowView))
        {
            return;
        }

        Vector3 exitWorldPosition = GetExitWorldPosition(arrowData);

        arrowView.PlayExitAnimation(exitWorldPosition, () =>
        {
            arrowViewsById.Remove(arrowData.Id);
            Destroy(arrowView.gameObject);
            gameController.ConfirmArrowRemoved(arrowData.Id);
        });
    }

    private void HandleLevelCompleted()
    {
        Debug.Log("Level Completed!");
    }

    public Vector3 GridToWorld(GridPosition gridPosition)
    {
        return boardOrigin + new Vector3(gridPosition.X * cellSize, gridPosition.Y * cellSize, 0f);
    }

    private Vector3 GetExitWorldPosition(ArrowData arrowData)
    {
        GridPosition exitGridPosition = BoardRules.GetExitGridPosition(boardState, arrowData);
        Vector3 boardExitWorldPosition = GridToWorld(exitGridPosition);
        Vector3 extraOffset = DirectionUtility.ToWorldDirection(arrowData.Direction) * (cellSize * exitDistanceInCells);

        return boardExitWorldPosition + extraOffset;
    }
}
