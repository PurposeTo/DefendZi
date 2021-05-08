using Desdiene.AtomicReference;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyPresenter : MonoBehaviour
{
    public EnemyMovement EnemyMovement => enemyMovementRef.Get(InitEnemyMovement);
    public EnemyHealth EnemyHealth => enemyHealthRef.Get(InitEnemyHealth);
    public EnemyAttack EnemyAttack => enemyAttackRef.Get(InitEnemyAttack);

    private readonly AtomicRefRuntimeInit<EnemyMovement> enemyMovementRef = new AtomicRefRuntimeInit<EnemyMovement>();
    private readonly AtomicRefRuntimeInit<EnemyHealth> enemyHealthRef = new AtomicRefRuntimeInit<EnemyHealth>();
    private readonly AtomicRefRuntimeInit<EnemyAttack> enemyAttackRef = new AtomicRefRuntimeInit<EnemyAttack>();

    private void Awake()
    {
        enemyMovementRef.Initialize(InitEnemyMovement);
        enemyHealthRef.Initialize(InitEnemyHealth);
        enemyAttackRef.Initialize(InitEnemyAttack);
    }

    private EnemyMovement InitEnemyMovement()
    {
        return GetComponent<EnemyMovement>();
    }

    private EnemyHealth InitEnemyHealth()
    {
        return GetComponent<EnemyHealth>();
    }

    private EnemyAttack InitEnemyAttack()
    {
        return GetComponent<EnemyAttack>();
    }
}
