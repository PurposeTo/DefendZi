using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(TextView))]
[RequireComponent(typeof(RectTransform))]
public class PopUpScore : MonoBehaviourExt
{
    private TextView _textView;
    private RectTransform _rectTransform;

    protected override void AwakeExt()
    {
        _rectTransform = GetComponent<RectTransform>();
        _textView = GetComponent<TextView>();
    }

    public void SetText(string text) => _textView.SetText(text);

    public void SetFontSize(float fontSize)
    {
        _textView.SetFontSize(fontSize);
    }

    public void SetFontAutoSize()
    {
        SetOverflowRectTransform();
        _textView.SetFontAutoSize();
    }

    // Метод вызывается unity animator-ом
    private void Destroy() => Destroy(gameObject);

    private void SetOverflowRectTransform()
    {
        _rectTransform.anchorMin = Vector2.zero;
        _rectTransform.anchorMax = Vector2.one;
        _rectTransform.offsetMin = Vector2.zero;
        _rectTransform.offsetMax = Vector2.zero;
        Vector2 middleVector = new Vector2(0.5f, 0.5f);
        _rectTransform.pivot = middleVector;
        _rectTransform.rotation = Quaternion.identity;
    }
}
