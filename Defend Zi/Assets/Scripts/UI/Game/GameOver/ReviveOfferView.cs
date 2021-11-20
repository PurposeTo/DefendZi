using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class ReviveOfferView : FullScreenWindow
{
    [SerializeField, NotNull] private TextView _scoreText;
    [SerializeField, NotNull] private Button _reviveForAdButton;
    [SerializeField, NotNull] private Button _refuseToRevivingButton;

    protected override void AwakeWindow()
    {
        _reviveForAdButton.onClick.AddListener(() => OnReviveForAdClicked?.Invoke());
        _refuseToRevivingButton.onClick.AddListener(() => OnRefuseToRevivingClicked?.Invoke());
    }

    public event Action OnReviveForAdClicked;
    public event Action OnRefuseToRevivingClicked;

    public void Init(uint score)
    {
        SetScore(score);
    }

    private void SetScore(uint score)
    {
        _scoreText.SetText($"{score}");
    }
}
