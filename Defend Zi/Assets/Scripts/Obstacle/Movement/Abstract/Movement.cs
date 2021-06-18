﻿using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public abstract class Movement : MonoBehaviourExt
{
    [SerializeField] private float _speed;
    protected float Speed => _speed;

    private void FixedUpdate()
    {
        Move(Time.fixedDeltaTime);
    }

    protected abstract void Move(float deltaTime);
}