using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionUtility
{
    public static GridPosition ToGridOffset(Direction direction)
    {
        return direction switch
        {
            Direction.Up => new GridPosition(0, 1),
            Direction.Down => new GridPosition(0, -1),
            Direction.Left => new GridPosition(-1, 0),
            Direction.Right => new GridPosition(1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static Vector3 ToWorldDirection(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Vector3.up,
            Direction.Down => Vector3.down,
            Direction.Left => Vector3.left,
            Direction.Right => Vector3.right,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static float ToZRotation(Direction direction)
    {
        return direction switch
        {
            Direction.Right => 0f,
            Direction.Down => -90f,
            Direction.Left => 180f,
            Direction.Up => 90f,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}
