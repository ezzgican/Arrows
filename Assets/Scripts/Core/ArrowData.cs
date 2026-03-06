using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ArrowData
{
    public int Id { get; }
    public GridPosition Position { get; }
    public Direction Direction { get; }

    public ArrowData(int id, GridPosition position, Direction direction)
    {
        Id = id;
        Position = position;
        Direction = direction;
    }
}
