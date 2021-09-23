using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class ReviveOfferView : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameObject _screen;
    [SerializeField, NotNull] private TextView _scoreText;

    public void Show(int score)
    {
        _screen.SetActive(true);
        SetScore(score);
    }

    public void Hide()
    {
        _screen.SetActive(false);
    }

    private void SetScore(int score)
    {
        _scoreText.SetText($"Score: {score}");
    }
}
