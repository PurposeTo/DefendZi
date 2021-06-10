using TMPro;
using UnityEngine;

public class DebugUIView : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void SetText(string str)
    {
        text.text = str;
    }
}
