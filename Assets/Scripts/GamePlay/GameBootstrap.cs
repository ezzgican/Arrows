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
        Debug.Log("GameBootstrap Start called");

        if (levelData == null)
        {
            
            return;
        }

        if (gameController == null)
        {
           
            return;
        }

        if (boardView == null)
        {
            
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
