using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 18;

    public event Action OnIsUnderControlChange;
    public bool IsUnderControl
    {
        get => isUnderControl;
        private set
        {
            if (isUnderControl != value)
            {
                isUnderControl = value;
                OnIsUnderControlChange?.Invoke();
            }
        }
    }
    public IPercentStat ZiPlayerDistance => rotationRadius;

    private FloatStatPercentable rotationRadius;
    private bool isUnderControl;

    private Zi Zi => GameObjectsHolder.Instance.ZiPresenter.Zi;
    private Rigidbody2D rb2d;
    private Controller controller;

    private float angle;

    private void Awake()
    {
        controller = ControllerInitializer.Initialize();
        rb2d = GetComponent<Rigidbody2D>();
        rotationRadius = new FloatStatPercentable(transform.position.magnitude, Zi.Radius + 1f, Zi.Radius + 20f);
    }

    private void Update()
    {
        IsUnderControl = controller.IsActive;
        rotationRadius.Set(GetNewRadius(Time.deltaTime));
        rb2d.MovePosition(GetNewPosition(Time.deltaTime));
    }

    private float GetNewRadius(float deltaTime)
    {
        float ZiGravity = Zi.GetZiGravity(transform.position).magnitude * -1 * deltaTime;
        float direction = IsUnderControl
            ? -1f
            : 1;
        return rotationRadius.Value + (ZiGravity * direction);
    }

    private Vector3 GetNewPosition(float deltaTime)
    {
        float angularSpeed = rotationSpeed / rotationRadius.Value * deltaTime;
        angle += angularSpeed;
        var rotationOffset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * rotationRadius.Value;
        return (Vector2)Zi.transform.position + rotationOffset;
    }
}
