using Desdiene.AnimatorExtension;
using UnityEngine;

/// <summary>
/// Класс описывает анимацию ауры игрока.
/// Использовать класс как модель.
/// 
/// Состояния:
/// Idle
/// Reinforced
/// ToIdleFromReinforced
/// 
/// </summary>
[RequireComponent(typeof(Animator))]
public class PlayerAuraAnimator : AnimatorModel
{
    private readonly string isReinforcedField = "reinforce";
    private AnimatorTrigger _reinforce;

    protected override void AwakeAnimator()
    {
        _reinforce = GetAnimatorTrigger(isReinforcedField);
    }

    public void Reinforce() => _reinforce.Trigger();
}
