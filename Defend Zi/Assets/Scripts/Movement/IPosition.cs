using UnityEngine;

public interface IPosition
{
    Vector2 GetPosition();
    void MoveTo(Vector2 vector);
}
