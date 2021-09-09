using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class Training : MonoBehaviourExt
{
    [SerializeField, NotNull] private FromTransparentToBlinking _blinking;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _blinking.Enable();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            _blinking.Disable();
        }
    }
}