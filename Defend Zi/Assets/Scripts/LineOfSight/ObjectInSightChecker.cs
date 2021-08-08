using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

/// <summary>
/// Данный класс вызывает событие, когда коллайдер выходит из поля зрения.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class ObjectInSightChecker : MonoBehaviourExt
{
    public event Action OnOutOfSight;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out GameSpaceInSight _))
        {
            OnOutOfSight?.Invoke();
        }
    }
}
