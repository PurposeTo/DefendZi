﻿using UnityEngine;

[RequireComponent(typeof(IPosition))]
public class PlayerControl : UserControlled, IUserControlled
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float amplitude = 6f;
    [SerializeField] private float defaultFrequency = 0.15f;
    [SerializeField] private float controlledFrequency = 0.5f;
    [SerializeField] private float frequencyChangeRate = 1.5f;

    private bool isControlled = false;
    private float frequency;
    private float phase;

    private IPosition position;

    protected override void AwakeExt()
    {
        base.AwakeExt(); //инициализация UserControlled
        position = GetComponent<IPosition>();
        frequency = defaultFrequency;
    }

    private void FixedUpdate()
    {
        frequency = isControlled
            ? GetFrequency(controlledFrequency, Time.fixedDeltaTime)
            : GetFrequency(defaultFrequency, Time.fixedDeltaTime);

        Move(Time.fixedDeltaTime);
    }

    public override void Control(IUserInput input)
    {
        isControlled = input.IsActive;
    }

    private void Move(float deltaTime)
    {
        float x = MoveOx(speed * deltaTime);
        float y = MoveOy(x);
        position.MoveTo(new Vector2(x, y));
    }

    private float MoveOx(float speed)
    {
        return position.Value.x + speed;
    }

    private float MoveOy(float x)
    {
        return amplitude * Mathf.Sin(frequency * x + phase);
    }

    private float GetFrequency(float targetFrequency, float deltaTime)
    {
        float delta = frequencyChangeRate * deltaTime;
        float current = frequency;
        float next = Mathf.MoveTowards(current, targetFrequency, delta);
        phase = GetPhase(current, next);
        return next;
    }

    private float GetPhase(float currentFrequency, float nextFrequency)
    {
        Vector2 position = this.position.Value;
        float current = (position.x * currentFrequency + phase) % (2f * Mathf.PI);
        float next = (position.x * nextFrequency) % (2f * Mathf.PI);
        return current - next;
    }
}
