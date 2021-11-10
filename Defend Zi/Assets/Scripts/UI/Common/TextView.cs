using Desdiene.MonoBehaviourExtension;
using TMPro;
using UnityEngine;

public class TextView : MonoBehaviourExt
{
    [SerializeField] private TMP_Text tmpText;

    public void SetText(string text)
    {
        tmpText.text = text;
    }

    public void SetFontSize(float fontSize)
    {
        tmpText.enableAutoSizing = false;
        tmpText.fontSize = fontSize;
    }

    public void SetFontAutoSize()
    {
        tmpText.enableAutoSizing = true;
    }

    public void SetFontAutoSize(float minFontSize, float maxFontSize)
    {
        tmpText.enableAutoSizing = true;
        tmpText.fontSizeMin = minFontSize;
        tmpText.fontSizeMax = maxFontSize;
    }
}
