using Desdiene.Types.AtomicReference.RefRuntimeInit;
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

    private readonly RefRuntimeInit<PlayerMovement> playerMovementRef = new RefRuntimeInit<PlayerMovement>();
    private readonly RefRuntimeInit<PlayerActivity> playerActivityRef = new RefRuntimeInit<PlayerActivity>();
    private readonly RefRuntimeInit<PlayerAttack> playerAttackRef = new RefRuntimeInit<PlayerAttack>();
    private readonly RefRuntimeInit<PlayerAura> playerAuraRef = new RefRuntimeInit<PlayerAura>();
    private readonly RefRuntimeInit<PlayerScore> playerScoreRef = new RefRuntimeInit<PlayerScore>();

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
