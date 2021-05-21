using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    private Zi Zi => GameObjectsHolder.Instance.ZiPresenter.Zi;
    private bool IsPlayerActive => GameObjectsHolder.Instance.PlayerPresenter.Activity.IsActive;

    [SerializeField]
    private float speed = 4f;

    private Rigidbody2D rb2d;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = Zi.GetToZiDirection(transform.position);

        //todo скорость врагов должна варьироваться от действий игрока?
        //float _speed = IsPlayerActive
        //    ? speed
        //    : 0;
        float _speed = speed;

        Vector2 velocity = direction * Mathf.MoveTowards(rb2d.velocity.magnitude, _speed, Time.fixedDeltaTime);
        rb2d.velocity = velocity;
    }
}
