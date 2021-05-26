using Desdiene.Types.AtomicReference.RefRuntimeInit;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyPresenter : MonoBehaviour
{
    public EnemyMovement EnemyMovement => enemyMovementRef.GetOrInit(InitEnemyMovement);
    public EnemyHealth EnemyHealth => enemyHealthRef.GetOrInit(InitEnemyHealth);
    public EnemyAttack EnemyAttack => enemyAttackRef.GetOrInit(InitEnemyAttack);

    private readonly RefRuntimeInit<EnemyMovement> enemyMovementRef = new RefRuntimeInit<EnemyMovement>();
    private readonly RefRuntimeInit<EnemyHealth> enemyHealthRef = new RefRuntimeInit<EnemyHealth>();
    private readonly RefRuntimeInit<EnemyAttack> enemyAttackRef = new RefRuntimeInit<EnemyAttack>();

    private void Awake()
    {
        enemyMovementRef.GetOrInit(InitEnemyMovement);
        enemyHealthRef.GetOrInit(InitEnemyHealth);
        enemyAttackRef.GetOrInit(InitEnemyAttack);
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
