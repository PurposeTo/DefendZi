using System;
using System.Collections;
using UnityEngine;

public class Observable_Test : MonoBehaviour
{
    public event Action OnTestEvent;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        OnTestEvent?.Invoke();
        Debug.Log($"{GetType().Name} Конец метода.");
    }
}
