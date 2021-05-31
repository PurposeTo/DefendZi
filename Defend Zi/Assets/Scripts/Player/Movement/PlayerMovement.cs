using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool isControlled = false;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float defaultFrequency = 0.5f;
    [SerializeField] private float controlledFrequency = 1f;
    [SerializeField] private float frequencyChangeRate = 1f;

    private float frequency;
    private float phase;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        frequency = defaultFrequency;
    }

    private void FixedUpdate()
    {
        frequency = ChangeFrequency(Time.fixedDeltaTime);
        Move(Time.fixedDeltaTime);
    }

    private void Move(float deltaTime)
    {
        float x = MoveOx(speed * deltaTime);
        float y = MoveOy(x);
        rb2d.MovePosition(new Vector2(x, y));
    }

    private float MoveOx(float speed)
    {
        Vector2 position = rb2d.position;
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
        float current = (rb2d.position.x * currentFrequency + phase) % (2f * Mathf.PI);
        float next = (rb2d.position.x * nextFrequency) % (2f * Mathf.PI);
        return current - next;
    }
}
