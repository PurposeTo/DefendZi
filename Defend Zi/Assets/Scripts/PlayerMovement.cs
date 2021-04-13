using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private float maxOrbit => GameObjectsHolder.Instance.Zi.Radius * 10f;
    private float minOrbit => GameObjectsHolder.Instance.Zi.Radius * 2f;

    private Rigidbody2D rb2d;

    [SerializeField]
    private float rotationVelocity = 18;
    [SerializeField]
    private float distanceVelocity = 5;
    
    private float currentOrbit;
    private float angle;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentOrbit = transform.position.magnitude;
    }

    private void Update()
    {
        currentOrbit = GetNewOrbit();
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(GetNewPosition());
    }

    private float GetNewOrbit()
    {
        float delta = Time.deltaTime * distanceVelocity;
        float _orbit = currentOrbit;

        if (Input.GetKey(KeyCode.Space))
        {
            _orbit += delta;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _orbit -= delta;
        }

        return Mathf.Clamp(_orbit, minOrbit, maxOrbit);
    }

    private Vector3 GetNewPosition()
    {
        Debug.Log(currentOrbit);
        float angularVelocity = rotationVelocity / currentOrbit * Time.fixedDeltaTime;
        angle += angularVelocity;
        var rotationOffset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * currentOrbit;
        return (Vector2)GameObjectsHolder.Instance.Zi.transform.position + rotationOffset;
    }
}
