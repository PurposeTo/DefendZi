using System.Collections;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class ScoreReceiverVfxController : MonoBehaviourExt
{
    [SerializeField, NotNull] private ScoreReceiver _scoreReceiver;
    [SerializeField, NotNull] private PopUpScore _popUpScorePrefab;

    protected override void AwakeExt()
    {
        SubcribeEvents();
    }

    private void OnDestroy()
    {
        UnsubcribeEvents();
    }

    private void CreatePopUp(int score)
    {
        PopUpScore popUpScore = Instantiate(_popUpScorePrefab, transform);
        popUpScore.SetText($"+{score}");
    }

    private void SubcribeEvents()
    {
        _scoreReceiver.OnReceived += CreatePopUp;
    }

    private void UnsubcribeEvents()
    {
        _scoreReceiver.OnReceived -= CreatePopUp;
    }
}
