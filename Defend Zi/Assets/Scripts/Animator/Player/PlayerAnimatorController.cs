using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviourExt
{
    [SerializeField, NotNull] InterfaceComponent<IScoreNotification> _scoreNotificationComponent;
    [SerializeField, NotNull] InterfaceComponent<IHealthNotification> _healthComponent;
    [SerializeField, NotNull] InterfaceComponent<IImmortalNotification> _immortalComponent;
    [SerializeField, NotNull] InterfaceComponent<IReincarnationNotification> _reincarnationComponent;
    [SerializeField, NotNull] PlayerAnimator _playerAnimator;

    protected override void AwakeExt()
    {
        SubscribeEvents();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    private IScoreNotification ScoreNotification => _scoreNotificationComponent.Implementation;
    private IHealthNotification Health => _healthComponent.Implementation;
    private IReincarnationNotification Reincarnation => _reincarnationComponent.Implementation;
    private IImmortalNotification Immortal => _immortalComponent.Implementation;

    private void SubscribeEvents()
    {
        ScoreNotification.OnReceived += ReinforceAure;
        Health.OnDeath += _playerAnimator.Die;
        Reincarnation.OnReviving += _playerAnimator.Revive;
        Immortal.WhenImmortal += _playerAnimator.EnableImmortality;
        Immortal.WhenMortal += _playerAnimator.DisableImmortality;
    }

    private void UnsubscribeEvents()
    {
        ScoreNotification.OnReceived -= ReinforceAure;
    }

    private void ReinforceAure(uint scoreReceived) => _playerAnimator.ReinforceAure();
}
