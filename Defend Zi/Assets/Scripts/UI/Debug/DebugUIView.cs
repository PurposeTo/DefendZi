using TMPro;
using UnityEngine;

public class DebugUIView : MonoBehaviour
{
    [SerializeField, NotNull] private TMP_Text text;

    public void SetText(string str)
    {
        text.text = str;
    }
}
