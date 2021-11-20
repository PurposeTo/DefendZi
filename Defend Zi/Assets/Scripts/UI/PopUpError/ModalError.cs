using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class ModalError : ModalWindow
{
    [SerializeField, NotNull] private TextView _message;
    [SerializeField, NotNull] private Button _closeButton;
    [SerializeField, NotNull] private Button _mainMenuButton;

    protected override void AwakeWindow()
    {
        _closeButton.onClick.AddListener(() => Hide());
        _mainMenuButton.onClick.AddListener(() => OnMainMenuClicked?.Invoke());
    }

    public void Init(string message)
    {
        _message.SetText(message);
    }

    public event Action OnMainMenuClicked;
}
