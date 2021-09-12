using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class Training : MonoBehaviourExt
{
    [SerializeField, NotNull] private FromTransparentToBlinking _blinking;
    [SerializeField, NotNull] private ToVisible _toVisible;

    protected override void AwakeExt()
    {

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _blinking.Enable();
            _toVisible.Enable();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            _blinking.Disable();
            _toVisible.Disable();
        }
    }
}
