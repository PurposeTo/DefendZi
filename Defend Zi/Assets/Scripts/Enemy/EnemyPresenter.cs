using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyPresenter : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private EnemyHealth enemyHealth;
    private EnemyAttack enemyAttack;

    private void Awake()
    {
        enemyMovement = GetEnemyMovement();
        enemyHealth = GetEnemyHealth();
        enemyAttack = GetEnemyAttack();
    }

    public EnemyMovement GetEnemyMovement()
    {
        if (enemyMovement == null) enemyMovement = GetComponent<EnemyMovement>();
        return enemyMovement;
    }

    public EnemyHealth GetEnemyHealth()
    {
        if (enemyHealth == null) enemyHealth = GetComponent<EnemyHealth>();
        return enemyHealth;
    }

    public EnemyAttack GetEnemyAttack()
    {
        if (enemyAttack == null) enemyAttack = GetComponent<EnemyAttack>();
        return enemyAttack;
    }
}
