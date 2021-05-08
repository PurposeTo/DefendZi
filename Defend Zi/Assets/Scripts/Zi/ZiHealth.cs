using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ZiHealth : MonoBehaviour
{
    private IntStatPercentable health = new IntStatPercentable(3, 0, 3);

    public IPercentStat GetHealthPercent()
    {
        return health;
    }

    public void TakeDamage()
    {
        health -= 1;
    }
}
