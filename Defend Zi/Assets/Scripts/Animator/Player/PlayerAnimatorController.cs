using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviourExt
{
    [SerializeField, NotNull] InterfaceComponent<IScoreNotification> _scoreNotificationComponent;
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

    private void SubscribeEvents()
    {
        ScoreNotification.OnReceived += ReinforceAure;
    }

    private void UnsubscribeEvents()
    {
        ScoreNotification.OnReceived -= ReinforceAure;
    }

    private void ReinforceAure(int scoreReceived) => _playerAnimator.ReinforceAure();
}
