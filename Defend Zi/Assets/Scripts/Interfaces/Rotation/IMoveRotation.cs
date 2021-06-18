using UnityEngine;

public interface IMoveRotation
{
    /// <summary>
    /// Повернуть на конечный угол.
    /// </summary>
    /// <param name="finalQuaternion">Конечный угол, на который необходимо повернуть объект.</param>
    void RotateTo(Quaternion finalQuaternion);

    /// <summary>
    /// Повернуть с текущего угла на дельту.
    /// </summary>
    /// <param name="deltaQuaternion">Дельта угла, на который необходимо повернуть объект.</param>
    void RotateBy(Quaternion deltaQuaternion);
}
