using System;
using Desdiene.Types.AtomicReference.Api;
using UnityEngine;

public class PlayerScore : MonoBehaviour, IReadRef<int>
{
    private int score;

    public event Action OnValueChanged;

    public void AddScore(int amount)
    {
        score += amount;
        OnValueChanged?.Invoke();
    }

    public int Get() => score;
}
