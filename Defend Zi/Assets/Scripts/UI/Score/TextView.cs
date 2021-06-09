using TMPro;
using UnityEngine;
using Desdiene.MonoBehaviourExtension;

public class TextView : MonoBehaviourExt
{
    [SerializeField] private TMP_Text scoreText;

    public void SetText(string text)
    {
        scoreText.text = text;
    }
}
