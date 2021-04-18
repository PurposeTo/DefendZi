using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyHealth : MonoBehaviour
{
    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}
