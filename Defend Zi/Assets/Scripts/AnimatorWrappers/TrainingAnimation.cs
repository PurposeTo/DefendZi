using System.Collections;
using System.Collections.Generic;
using Desdiene.AnimatorExtension;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TrainingAnimation : MonoBehaviourExt
{
    private readonly string isEnabledField = "isEnabled";
    private Animator _animator;
    private AnimatorBool _isEnabled;

    protected override void AwakeExt()
    {
        _animator = GetComponent<Animator>();
        AnimatorParameters animatorParameters = new AnimatorParameters(_animator);
        _isEnabled = new AnimatorBool(_animator, animatorParameters, isEnabledField);
    }
}
