using UnityEngine;

public class PlayerControl : MonoBehaviour, IUserControllable
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float defaultFrequency = 0.5f;
    [SerializeField] private float controlledFrequency = 1f;
    [SerializeField] private float frequencyChangeRate = 1f;

    private bool isControlled = false;
    private float frequency;
    private float phase;

    private IPosition position;

    private void Awake()
    {
        frequency = defaultFrequency;
    }

    private void FixedUpdate()
    {
        frequency = ChangeFrequency(Time.fixedDeltaTime);
        Move(Time.fixedDeltaTime);
    }

    public PlayerControl Constructor(IPosition position)
    {
        this.position = position;
        return this;
    }

    void IUserControllable.Control(IUserInput input)
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
        Vector2 position = this.position.GetPosition();
        position.x += speed;
        return position.x;
    }

    private float MoveOy(float x)
    {
        return amplitude * Mathf.Sin(frequency * x + phase);
    }

    private float ChangeFrequency(float deltaTime)
    {
        float delta = frequencyChangeRate * deltaTime;
        float targetFrequency = isControlled
            ? controlledFrequency
            : defaultFrequency;

        float current = frequency;
        float next = Mathf.MoveTowards(current, targetFrequency, delta);
        phase = ChangePhase(current, next);
        return next;
    }

    private float ChangePhase(float currentFrequency, float nextFrequency)
    {
        Vector2 position = this.position.GetPosition();
        float current = (position.x * currentFrequency + phase) % (2f * Mathf.PI);
        float next = (position.x * nextFrequency) % (2f * Mathf.PI);
        return current - next;
    }
}
