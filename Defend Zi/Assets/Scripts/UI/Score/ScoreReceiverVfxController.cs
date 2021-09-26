using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class ScoreReceiverVfxController : MonoBehaviourExt
{
    [SerializeField, NotNull] private InterfaceComponent<IScoreNotification> _scoreNotification;
    [SerializeField, NotNull] private PopUpScore _popUpScorePrefab;

    protected override void AwakeExt()
    {
        SubcribeEvents();
    }

    private void OnDestroy()
    {
        UnsubcribeEvents();
    }

    private IScoreNotification ScoreNotification => _scoreNotification.Implementation;

    private void CreatePopUp(int score)
    {
        PopUpScore popUpScore = Instantiate(_popUpScorePrefab, transform);
        popUpScore.SetText($"+{score}");
    }

    private void SubcribeEvents()
    {
        ScoreNotification.OnReceived += CreatePopUp;
    }

    private void UnsubcribeEvents()
    {
        ScoreNotification.OnReceived -= CreatePopUp;
    }
}
