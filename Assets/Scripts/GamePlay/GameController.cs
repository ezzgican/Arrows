using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public event Action<ArrowData> OnArrowRemovalStarted;
    public event Action OnLevelCompleted;

    private BoardState boardState;
    private bool isInitialized;
    private bool isBusy;

    public BoardState BoardState => boardState;

    public void Initialize(BoardState initialBoardState)
    {
        boardState = initialBoardState;
        isInitialized = true;
        isBusy = false;
    }

    public bool CanSelectArrow(int arrowId)
    {
        if (!isInitialized || isBusy)
        {
            return false;
        }

        ArrowData arrow = boardState.GetArrowById(arrowId);
        if (arrow == null)
        {
            return false;
        }

        return BoardRules.CanArrowExit(boardState, arrow);
    }

    public bool TryRemoveArrow(int arrowId)
    {
        if (!isInitialized || isBusy)
        {
           
            return false;
        }

        ArrowData arrow = boardState.GetArrowById(arrowId);
        if (arrow == null)
        {
            
            return false;
        }

       

        if (!BoardRules.CanArrowExit(boardState, arrow))
        {
           
            return false;
        }

        
        isBusy = true;
        OnArrowRemovalStarted?.Invoke(arrow);
        return true;
    }

    public void ConfirmArrowRemoved(int arrowId)
    {
        if (!isInitialized)
        {
            return;
        }

        ArrowData arrow = boardState.GetArrowById(arrowId);
        if (arrow == null)
        {
            isBusy = false;
            return;
        }

        boardState.RemoveArrow(arrow);
        isBusy = false;

        if (BoardRules.IsLevelComplete(boardState))
        {
            OnLevelCompleted?.Invoke();
        }
    }
}
