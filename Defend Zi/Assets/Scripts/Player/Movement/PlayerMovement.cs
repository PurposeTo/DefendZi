using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool isControlled = false;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float defaultSinusoidRate = 1f;
    [SerializeField] private float controlledSinusoidRate = 3f;
    [SerializeField] private float deltaSinusoidRate = 5f;

    private float currentRate = 1f;

    private Rigidbody2D rb2d;


    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        float newOxPosition = rb2d.transform.position.x + speed;
        float newOyPosition;

        if (isControlled)
        {
            currentRate = Mathf.MoveTowards(currentRate, controlledSinusoidRate, deltaSinusoidRate * Time.fixedDeltaTime);
        }
        else
        {
            currentRate = Mathf.MoveTowards(currentRate, defaultSinusoidRate, deltaSinusoidRate * Time.fixedDeltaTime);
        }

        newOyPosition = Mathf.Sin(currentRate * newOxPosition);
        rb2d.MovePosition(new Vector2(newOxPosition, newOyPosition));
    }


    private void OnIsControlled(bool isControlled)
    {
        this.isControlled = isControlled;
        if (isControlled) currentRate = defaultSinusoidRate;
        else currentRate = controlledSinusoidRate;
    }
}
