using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class ReviveOfferView : FullScreenWindow
{
    [SerializeField, NotNull] private TextView _scoreText;
    [SerializeField, NotNull] private Button _reviveForAdButton;
    [SerializeField, NotNull] private Button _refuseToRevivingButton;
    [SerializeField, NotNull] private Button _mainMenuButton;

    protected override void AwakeWindow()
    {
        _reviveForAdButton.onClick.AddListener(() => OnReviveForAdClicked?.Invoke());
        _refuseToRevivingButton.onClick.AddListener(() => OnRefuseToRevivingClicked?.Invoke());
        _mainMenuButton.onClick.AddListener(() => OnMainMenuClicked?.Invoke());
    }

    public event Action OnReviveForAdClicked;
    public event Action OnRefuseToRevivingClicked;
    public event Action OnMainMenuClicked;

    public void Init(int score)
    {
        SetScore(score);
    }

    private void SetScore(int score)
    {
        _scoreText.SetText($"Score: {score}");
    }
}
