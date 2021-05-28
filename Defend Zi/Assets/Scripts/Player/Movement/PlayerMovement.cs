using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool isControlled = false;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float amplitudeOy = 4f;
    private float counter = 0f;

    private Rigidbody2D rb2d;


    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        Move(Time.fixedDeltaTime);
    }


    private void Move(float deltaTime)
    {
        float y = MoveToPingPong(speed * deltaTime);
        rb2d.MovePosition(new Vector2(0f, y));
    }

 
    private float MoveToPingPong(float speed)
    {
        counter += speed;
        float targetPosition = PingPongNegativeToPositive(counter, amplitudeOy);
        return Mathf.MoveTowards(rb2d.position.y, targetPosition, speed);
    }


    /// <summary>
    /// PingPong returns a value that will increment and decrement between the value -target and target.
    /// </summary>
    private float PingPongNegativeToPositive(float t, float target)
    {
        return Mathf.PingPong(t, 2 * target) - target;
    }
}
