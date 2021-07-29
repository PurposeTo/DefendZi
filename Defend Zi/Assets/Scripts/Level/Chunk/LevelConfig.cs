using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Данный класс описывает настройки игрового уровня.
/// </summary>
public class LevelConfig
{
    public static float BottomPoint => YAxisStart - (Height / 2f); // нижняя точка уровня.
    public static float TopPoint => YAxisStart + (Height / 2f); // верхняя точка уровня.
    public const float Height = 15; //Высота уровня (Как длина вектора. Строго положительное число).
    public const float YAxisStart = 0f; // начало Y оси уровня.
    public const float XAxisStart = 0f; // начало X оси уровня.
    public const float ZAxisStart = 0f; // начало Z оси уровня.
}
