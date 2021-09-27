using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(TextView))]
public class PopUpScore : MonoBehaviourExt
{
    private TextView _textView;

    protected override void AwakeExt()
    {
        _textView = GetComponent<TextView>();
    }

    public void SetText(string text) => _textView.SetText(text);

    // Ìועמה גûחûגאועסÿ unity animator-מל
    private void Destroy() => Destroy(gameObject);
}
