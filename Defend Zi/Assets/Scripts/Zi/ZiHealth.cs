using System;
using Desdiene.Types.AtomicReference.Interfaces;
using Desdiene.Types.RangeType;
using Desdiene.Types.ValuesInRange;
using Desdiene.Types.ValuesInRange.Interfaces;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ZiHealth : MonoBehaviour
{
    public event Action OnZiDie;

    public IReadPercentable GetHealthPercent() => health;
    public IReadRef<int> GetHealth() => health;
    private IntPercentable health = new IntPercentable(3, new Range<int>(0, 3));

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
