using UnityEngine;

public class PlayerControl
{
    private readonly IUserInput userInput;
    private readonly IPosition position;
    private readonly PlayerMovementData controlData;

    public PlayerControl(IUserInput input, IPosition position, PlayerMovementData controlData)
    {
        userInput = input ?? throw new System.ArgumentNullException(nameof(input));
        this.position = position ?? throw new System.ArgumentNullException(nameof(position));
        this.controlData = controlData
            ? controlData 
            : throw new System.ArgumentNullException(nameof(controlData));
        frequency = controlData.defaultFrequency;
    }

    private bool IsControlled => userInput.IsActive;
    private float frequency;
    private float phase;

    public void FixedUpdate(float deltaTime)
    {
        float targetFrequency = IsControlled
            ? controlData.controlledFrequency
            : controlData.defaultFrequency;
        frequency = GetFrequency(targetFrequency, deltaTime);
        Move(deltaTime);
    }

    private void Move(float deltaTime)
    {
        float x = MoveOx(controlData.speed * deltaTime);
        float y = MoveOy(x);
        position.MoveTo(new Vector2(x, y));
    }

    private float MoveOx(float speed)
    {
        return position.Value.x + speed;
    }

    private float MoveOy(float x)
    {
        return controlData.amplitude * Mathf.Sin(frequency * x + phase);
    }

    private float GetFrequency(float targetFrequency, float deltaTime)
    {
        float delta = controlData.frequencyChangeRate * deltaTime;
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
