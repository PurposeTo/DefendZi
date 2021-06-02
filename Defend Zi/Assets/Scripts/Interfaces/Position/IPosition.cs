using UnityEngine;

public interface IPosition
{
    Vector2 Value { get; }
    void MoveTo(Vector2 vector);
}
