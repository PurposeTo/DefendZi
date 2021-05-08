using Desdiene.AtomicReference;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerActivity))]
public class PlayerPresenter : MonoBehaviour
{
    public PlayerMovement Movement => playerMovementRef.Get(InitPlayerMovement);
    public PlayerActivity Activity => playerActivityRef.Get(InitPlayerActivity);
    public PlayerAttack Attack => playerAttackRef.Get(InitPlayerAttack);
    public PlayerAura Aura => playerAuraRef.Get(InitPlayerAura);

    private readonly AtomicRefRuntimeInit<PlayerMovement> playerMovementRef = new AtomicRefRuntimeInit<PlayerMovement>();
    private readonly AtomicRefRuntimeInit<PlayerActivity> playerActivityRef = new AtomicRefRuntimeInit<PlayerActivity>();
    private readonly AtomicRefRuntimeInit<PlayerAttack> playerAttackRef = new AtomicRefRuntimeInit<PlayerAttack>();
    private readonly AtomicRefRuntimeInit<PlayerAura> playerAuraRef = new AtomicRefRuntimeInit<PlayerAura>();

    private void Awake()
    {
        playerMovementRef.Initialize(InitPlayerMovement);
        playerActivityRef.Initialize(InitPlayerActivity);
        playerAttackRef.Initialize(InitPlayerAttack);
        playerAuraRef.Initialize(InitPlayerAura);
    }

    public PlayerMovement InitPlayerMovement()
    {
        return GetComponent<PlayerMovement>();
    }

    public PlayerActivity InitPlayerActivity()
    {
        return GetComponent<PlayerActivity>().Constructor(Movement, Aura);
    }

    public PlayerAttack InitPlayerAttack()
    {
        return GetComponentInChildren<PlayerAttack>();
    }

    public PlayerAura InitPlayerAura()
    {
        return GetComponentInChildren<PlayerAura>();
    }
}
