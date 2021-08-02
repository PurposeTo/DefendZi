﻿using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class PlayerControl : IFixedUpdate
{
    private readonly IUserInput _userInput;
    private readonly IPosition _position;
    private readonly PlayerMovementData _movementData;

    public PlayerControl(IUserInput input, IPosition position, PlayerMovementData movementData)
    {
        _userInput = input ?? throw new ArgumentNullException(nameof(input));
        _position = position ?? throw new ArgumentNullException(nameof(position));
        _movementData = movementData ?? throw new ArgumentNullException(nameof(movementData));
        _frequency = movementData.defaultFrequency;
    }

    private bool IsControlled => _userInput.IsActive;
    private float _frequency;
    private float _phase;

    void IFixedUpdate.Invoke(float deltaTime)
    {
        float targetFrequency = IsControlled
            ? _movementData.controlledFrequency
            : _movementData.defaultFrequency;
        _frequency = GetFrequency(targetFrequency, deltaTime);
        Move(deltaTime);
    }

    private void Move(float deltaTime)
    {
        float x = MoveOx(_movementData.speed * deltaTime);
        float y = MoveOy(x);
        _position.MoveTo(new Vector2(x, y));
    }

    private float MoveOx(float speed)
    {
        return _position.Value.x + speed;
    }

    private float MoveOy(float x)
    {
        return _movementData.amplitude * Mathf.Sin(_frequency * x + _phase);
    }

    private float GetFrequency(float targetFrequency, float deltaTime)
    {
        float delta = _movementData.frequencyChangeRate * deltaTime;
        float current = _frequency;
        float next = Mathf.MoveTowards(current, targetFrequency, delta);
        _phase = GetPhase(current, next);
        return next;
    }

    private float GetPhase(float currentFrequency, float nextFrequency)
    {
        Vector2 position = this._position.Value;
        float current = (position.x * currentFrequency + _phase) % (2f * Mathf.PI);
        float next = (position.x * nextFrequency) % (2f * Mathf.PI);
        return current - next;
    }
}
