using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class PlayerAnimator : MonoBehaviourExt
{
    [SerializeField, NotNull] PlayerAuraAnimator _playerAura;

    public void ReinforceAure() => _playerAura.Reinforce();

    public void Die() { }
    public void Revive() { }
    public void EnableImmortality() { }
    public void DisableImmortality() { }
}
