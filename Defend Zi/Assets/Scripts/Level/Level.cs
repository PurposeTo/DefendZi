using System.Collections;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

/// <summary>
/// Описывает уровень игры.
/// Состоит из препятствий.
/// </summary>
public class Level : ILevelSize
{
    //todo вынести конфиги в отдельный класс.
    #region конфиги уровня.
    public static float BottomPoint => YAxisStart - (Height / 2f); // нижняя точка уровня.
    public static float TopPoint => YAxisStart + (Height / 2f); // верхняя точка уровня.
    public const float Height = 15; //Высота уровня (Как длина вектора. Строго положительное число).
    public const float YAxisStart = 0f; // начало Y оси уровня.
    public const float XAxisStart = 0f; // начало X оси уровня.
    public const float ZAxisStart = 0f; // начало Z оси уровня.
    #endregion

    private float _width = 0f;

    float ILevelSize.Height => Height;

    float ILevelSize.Width => _width;
}
