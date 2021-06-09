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
}
