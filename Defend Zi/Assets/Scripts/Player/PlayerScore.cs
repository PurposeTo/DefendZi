using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour, IStat<int>
{
    private int score;

    public int Value => score;

    public event Action OnStatChange;

    public void AddScore(int amount)
    {
        score += amount;
        OnStatChange?.Invoke();
    }
}
