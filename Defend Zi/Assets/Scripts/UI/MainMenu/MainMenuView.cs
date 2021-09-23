using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameObject _mainMenuScreen;
    [SerializeField, NotNull] private Button _playButton;

    protected override void AwakeExt()
    {
        _playButton.onClick.AddListener(() => OnGameClicked?.Invoke());
    }

    public event Action OnGameClicked;
}
