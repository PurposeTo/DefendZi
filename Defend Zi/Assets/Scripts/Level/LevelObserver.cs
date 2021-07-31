using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

/// <summary>
/// Данный класс следит за наполненностью уровня и вызывает событие, когда необходимо сгенерировать новую часть уровня.
/// </summary>
// todo: должен ли данный класс показывать информацию о той области уровня, которая уже не используется, т.е. которую можно удалить?
public class LevelObserver
{
    private IPositionGetter _playerPosition; //генерировать чанки нужно по мере продвижения игрока
    private CameraSize _cameraSize; //чанки нужно генерировать вне зоны видимости

    public event Action OnNeedToGenerage;
}
