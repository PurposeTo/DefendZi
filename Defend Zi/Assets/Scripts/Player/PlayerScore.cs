using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour, IStat<int>
{
    private int score;

    public int Value => score;

    public event Action OnValueChanged;

    public void AddScore(int amount)
    {
        score += amount;
        OnValueChanged?.Invoke();
    }
}
