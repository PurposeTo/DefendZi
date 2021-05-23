using Desdiene.Types.AtomicReference.RefRuntimeInit;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyPresenter : MonoBehaviour
{
    public EnemyMovement EnemyMovement => enemyMovementRef.Get(InitEnemyMovement);
    public EnemyHealth EnemyHealth => enemyHealthRef.Get(InitEnemyHealth);
    public EnemyAttack EnemyAttack => enemyAttackRef.Get(InitEnemyAttack);

    private readonly RefRuntimeInit<EnemyMovement> enemyMovementRef = new RefRuntimeInit<EnemyMovement>();
    private readonly RefRuntimeInit<EnemyHealth> enemyHealthRef = new RefRuntimeInit<EnemyHealth>();
    private readonly RefRuntimeInit<EnemyAttack> enemyAttackRef = new RefRuntimeInit<EnemyAttack>();

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
