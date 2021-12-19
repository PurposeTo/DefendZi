using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class PlayerControl : MonoBehaviourExtContainer
{
    private readonly IUserInput _userInput;
    private readonly IPosition _position;
    private readonly PlayerMovementData _movementData;

    public PlayerControl(MonoBehaviourExt mono, IUserInput input, IPosition position, PlayerMovementData movementData) : base(mono)
    {
        _userInput = input ?? throw new ArgumentNullException(nameof(input));
        _position = position ?? throw new ArgumentNullException(nameof(position));
        _movementData = movementData ?? throw new ArgumentNullException(nameof(movementData));
        _frequency = movementData.DefaultFrequency;
    }

    private bool IsControlled => _userInput.IsActive;
    private float _frequency;
    private float _phase;

    protected override void FixedUpdateExt()
    {
        float deltaTime = Time.fixedDeltaTime;

        float targetFrequency = IsControlled
            ? _movementData.ControlledFrequency
            : _movementData.DefaultFrequency;
        _frequency = GetFrequency(targetFrequency, deltaTime);
        Move(deltaTime);
    }

    private void Move(float deltaTime)
    {
        float x = MoveOx(_movementData.Speed * deltaTime);
        float y = MoveOy(x);
        _position.MoveTo(new Vector2(x, y));
    }

    private float MoveOx(float speed)
    {
        return _position.Value.x + speed;
    }

    private float MoveOy(float x)
    {
        return _movementData.Amplitude * Mathf.Sin(_frequency * x + _phase);
    }

    private float GetFrequency(float targetFrequency, float deltaTime)
    {
        float delta = _movementData.FrequencyChangeRate * deltaTime;
        float current = _frequency;
        float next = Mathf.MoveTowards(current, targetFrequency, delta);
        _phase = GetPhase(current, next);
        return next;
    }

    private float GetPhase(float currentFrequency, float nextFrequency)
    {
        Vector2 position = _position.Value;
        float current = (position.x * currentFrequency + _phase) % (2f * Mathf.PI);
        float next = (position.x * nextFrequency) % (2f * Mathf.PI);
        return current - next;
    }
}
