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
    public PlayerScore Score => playerScoreRef.Get(InitPlayerScore);

    private readonly AtomicRefRuntimeInit<PlayerMovement> playerMovementRef = new AtomicRefRuntimeInit<PlayerMovement>();
    private readonly AtomicRefRuntimeInit<PlayerActivity> playerActivityRef = new AtomicRefRuntimeInit<PlayerActivity>();
    private readonly AtomicRefRuntimeInit<PlayerAttack> playerAttackRef = new AtomicRefRuntimeInit<PlayerAttack>();
    private readonly AtomicRefRuntimeInit<PlayerAura> playerAuraRef = new AtomicRefRuntimeInit<PlayerAura>();
    private readonly AtomicRefRuntimeInit<PlayerScore> playerScoreRef = new AtomicRefRuntimeInit<PlayerScore>();

    private void Awake()
    {
        playerMovementRef.Initialize(InitPlayerMovement);
        playerActivityRef.Initialize(InitPlayerActivity);
        playerAttackRef.Initialize(InitPlayerAttack);
        playerAuraRef.Initialize(InitPlayerAura);
    }

    private PlayerMovement InitPlayerMovement()
    {
        return GetComponent<PlayerMovement>();
    }

    private PlayerActivity InitPlayerActivity()
    {
        return GetComponent<PlayerActivity>().Constructor(Movement, Aura);
    }

    private PlayerAttack InitPlayerAttack()
    {
        return GetComponentInChildren<PlayerAttack>().Constructor(Score);
    }

    private PlayerAura InitPlayerAura()
    {
        return GetComponentInChildren<PlayerAura>();
    }

    private PlayerScore InitPlayerScore()
    {
        return GetComponentInChildren<PlayerScore>();
    }
}
