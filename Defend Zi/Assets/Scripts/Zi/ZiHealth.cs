using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ZiHealth : MonoBehaviour
{
    public event Action OnZiDie;

    public IPercentStat GetHealthPercent() => health;
    public IStat<int> GetHealth() => health;
    private IntStatPercentable health = new IntStatPercentable(3, 0, 3);

    public void TakeDamage()
    {
        health -= 1;
        if (health == 0) Die();
    }

    private void Die()
    {
        OnZiDie?.Invoke();
    }
}
