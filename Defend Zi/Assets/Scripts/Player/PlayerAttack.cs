using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage();
        }
    }
}
