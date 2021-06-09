﻿using TMPro;
using UnityEngine;
using Desdiene.MonoBehaviourExtension;

public class TextView : MonoBehaviourExt
{
    [SerializeField] private TMP_Text tmpText;

    public void SetText(string text)
    {
        tmpText.text = text;
    }
}
