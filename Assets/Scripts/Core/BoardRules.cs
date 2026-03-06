using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardRules
{
    public static bool CanArrowExit(BoardState boardState, ArrowData arrow)
    {
        if (boardState == null || arrow == null)
        {
            return false;
        }

        GridPosition step = DirectionUtility.ToGridOffset(arrow.Direction);
        GridPosition current = arrow.Position.Add(step);

        while (boardState.IsInside(current))
        {
            if (boardState.HasArrowAt(current))
            {
                return false;
            }

            current = current.Add(step);
        }

        return true;
    }
    public static bool IsLevelComplete(BoardState boardState)
    {
        return boardState != null && boardState.GetArrowCount() == 0;
    }

    public static GridPosition GetExitGridPosition(BoardState boardState, ArrowData arrow)
    {
        GridPosition step = DirectionUtility.ToGridOffset(arrow.Direction);
        GridPosition current = arrow.Position.Add(step);

        while (boardState.IsInside(current))
        {
            current = current.Add(step);
        }

        return current;
    }
}
