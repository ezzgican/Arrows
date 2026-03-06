using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public readonly struct GridPosition : IEquatable<GridPosition>
{
    public int X { get; }
    public int Y { get; }

    public GridPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public GridPosition Add(GridPosition other)
    {
        return new GridPosition(X + other.X, Y + other.Y);
    }

    public bool Equals(GridPosition other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}
