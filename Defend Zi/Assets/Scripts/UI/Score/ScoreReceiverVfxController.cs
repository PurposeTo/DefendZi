using Desdiene.MonoBehaviourExtension;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreReceiverVfxController : MonoBehaviourExt
{
    [SerializeField, NotNull] private InterfaceComponent<IScoreNotification> _scoreNotification;
    [SerializeField, NotNull] private PopUpScore _popUpScorePrefab;
    private TMP_Text _tmpTextTemplate;
    private float _fontSize;

    protected override void AwakeExt()
    {
        _tmpTextTemplate = GetComponent<TMP_Text>();
        _fontSize = _tmpTextTemplate.fontSize;
        SubcribeEvents();
    }

    private void OnDestroy()
    {
        UnsubcribeEvents();
    }

    private IScoreNotification ScoreNotification => _scoreNotification.Implementation;

    private void CreatePopUp(uint score)
    {
        PopUpScore popUpScore = Instantiate(_popUpScorePrefab, transform);
        popUpScore.SetFontSize(_fontSize);
        popUpScore.SetText($" +{score}");
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
