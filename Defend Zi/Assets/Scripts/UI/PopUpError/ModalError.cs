using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class ModalError : ModalWindow
{
    [SerializeField, NotNull] private TextView _message;
    private Button _closeButton;

    protected override void AwakeWindow()
    {
        _closeButton = GetComponent<Button>();
        _closeButton.onClick.AddListener(() => Hide());
    }

    public void Init(string message)
    {
        _message.SetText(message);
    }
}
