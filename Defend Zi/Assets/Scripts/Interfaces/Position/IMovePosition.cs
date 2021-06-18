using UnityEngine;

public interface IMovePosition
{
    /// <summary>
    /// Подвинуть к конечной позиции.
    /// </summary>
    /// <param name="finalPosition">Конечная позиция, куда необходимо подвинуть объект.</param>
    void MoveTo(Vector2 finalPosition);

    /// <summary>
    /// Подвинуть с текущей позиции на дельту.
    /// </summary>
    /// <param name="deltaPosition">Дельта позиции, на которую необходимо подвинуть объект.</param>
    void MoveBy(Vector2 deltaPosition);
}
