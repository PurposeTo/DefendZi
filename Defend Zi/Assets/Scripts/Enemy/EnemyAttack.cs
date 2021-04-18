using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyAttack : MonoBehaviour
{
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out ZiHealth ziHealth))
        {
            ziHealth.TakeDamage();
            enemyHealth.TakeDamage();
        }
    }
}
