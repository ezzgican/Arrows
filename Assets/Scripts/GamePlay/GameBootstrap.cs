using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private LevelDataSO levelData;
    [SerializeField] private GameController gameController;
    [SerializeField] private BoardView boardView;

    private void Start()
    {
        if (levelData == null)
        {
            Debug.LogError("GameBootstrap: LevelDataSO reference is missing.");
            return;
        }

        if (gameController == null)
        {
            Debug.LogError("GameBootstrap: GameController reference is missing.");
            return;
        }

        if (boardView == null)
        {
            Debug.LogError("GameBootstrap: BoardView reference is missing.");
            return;
        }

        List<ArrowData> arrows = BuildArrowDataList(levelData);
        BoardState boardState = new BoardState(levelData.width, levelData.height, arrows);

        gameController.Initialize(boardState);
        boardView.Initialize(boardState, gameController);
    }

    private List<ArrowData> BuildArrowDataList(LevelDataSO data)
    {
        List<ArrowData> arrows = new List<ArrowData>(data.arrows.Count);

        for (int i = 0; i < data.arrows.Count; i++)
        {
            ArrowSpawnData spawnData = data.arrows[i];

            ArrowData arrow = new ArrowData(
                i,
                new GridPosition(spawnData.position.x, spawnData.position.y),
                spawnData.direction
            );

            arrows.Add(arrow);
        }

        return arrows;
    }
}
