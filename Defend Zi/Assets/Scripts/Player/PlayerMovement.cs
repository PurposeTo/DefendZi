using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public event Action OnIsUnderControlChange;
    public bool IsUnderControl
    {
        get => isUnderControl; 
        private set
        {
            isUnderControl = value;
            OnIsUnderControlChange?.Invoke();
        }
    }
    private bool isUnderControl;

    private Zi Zi => GameObjectsHolder.Instance.Zi;
    private float MaxRadius => Zi.Radius + 20f;
    private float MinRadius => Zi.Radius + 1f;

    private Rigidbody2D rb2d;

    [SerializeField]
    private float rotationSpeed = 18;

    private float currentRadius;
    private float angle;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentRadius = transform.position.magnitude;
    }

    private void Update()
    {
        IsUnderControl = Input.GetKey(KeyCode.Space);
        currentRadius = GetNewRadius(Time.deltaTime);
        rb2d.MovePosition(GetNewPosition(Time.deltaTime));

    }
    private float GetNewRadius(float deltaTime)
    {
        float ZiGravity = Zi.GetZiGravity(transform.position).magnitude * -1 * deltaTime;
        float _orbit = currentRadius;
        float direction = IsUnderControl
            ? -1f
            : 1;
        _orbit += ZiGravity * direction;

        return Mathf.Clamp(_orbit, MinRadius, MaxRadius);
    }

    private Vector3 GetNewPosition(float deltaTime)
    {
        float angularSpeed = rotationSpeed / currentRadius * deltaTime;
        angle += angularSpeed;
        var rotationOffset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * currentRadius;
        return (Vector2)GameObjectsHolder.Instance.Zi.transform.position + rotationOffset;
    }
}
