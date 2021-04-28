using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ZiHealth : MonoBehaviour
{
    private readonly IntStatPercentable health = new IntStatPercentable(3, 0, 3);

    public float GetHealthPercent()
    {
        return health.GetPercent();
    }

    public void TakeDamage()
    {
        health.Set(health.Value - 1);
    }
}
