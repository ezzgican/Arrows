using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BoardState
{
    private readonly int width;
    private readonly int height;
    private readonly Dictionary<GridPosition, ArrowData> arrowsByPosition;
    private readonly Dictionary<int, ArrowData> arrowsById;

    public int Width => width;
    public int Height => height;

    public BoardState(int width, int height, IEnumerable<ArrowData> arrows)
    {
        this.width = width;
        this.height = height;

        arrowsByPosition = new Dictionary<GridPosition, ArrowData>();
        arrowsById = new Dictionary<int, ArrowData>();

        foreach (ArrowData arrow in arrows)
        {
            arrowsByPosition.Add(arrow.Position, arrow);
            arrowsById.Add(arrow.Id, arrow);
        }
    }

    public bool IsInside(GridPosition position)
    {
        return position.X >= 0 && position.X < width &&
               position.Y >= 0 && position.Y < height;
    }

    public bool HasArrowAt(GridPosition position)
    {
        return arrowsByPosition.ContainsKey(position);
    }

    public ArrowData GetArrowAt(GridPosition position)
    {
        arrowsByPosition.TryGetValue(position, out ArrowData arrow);
        return arrow;
    }

    public ArrowData GetArrowById(int id)
    {
        arrowsById.TryGetValue(id, out ArrowData arrow);
        return arrow;
    }

    public IReadOnlyCollection<ArrowData> GetAllArrows()
    {
        return arrowsById.Values;
    }

    public int GetArrowCount()
    {
        return arrowsById.Count;
    }

    public void RemoveArrow(ArrowData arrow)
    {
        if (arrow == null)
        {
            return;
        }

        arrowsByPosition.Remove(arrow.Position);
        arrowsById.Remove(arrow.Id);
    }
}
