using Desdiene.AnimatorExtension;

public class PlayerAnimator : AnimatorModel
{
    private AnimatorTrigger _reinforceAura;
    private AnimatorBool _alive;
    private AnimatorBool _invulnerable;

    protected override void AwakeAnimator()
    {
        _reinforceAura = GetAnimatorTrigger("reinforceAura");
        _alive = GetAnimatorBool("alive", true);
        _invulnerable = GetAnimatorBool("invulnerable", false);
    }

    public void CollectScore()
    {
        _reinforceAura.Trigger();
    }

    public void Die()
    {
        _alive.Value = false;
    }

    public void Revive()
    {
        _alive.Value = true;
    }

    public void EnableInvulnerability()
    {
        _invulnerable.Value = true;
    }

    public void DisableInvulnerability()
    {
        _invulnerable.Value = false;
    }
}
