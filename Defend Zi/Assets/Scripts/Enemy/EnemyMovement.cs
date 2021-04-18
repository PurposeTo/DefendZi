using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    private Zi Zi => GameObjectsHolder.Instance.Zi;
    private bool IsPlayerActive => GameObjectsHolder.Instance.PlayerPresenter.GetPlayerActivity().IsActive;

    [SerializeField]
    private float speed = 2f;

    private Rigidbody2D rb2d;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = Zi.GetToZiDirection(transform.position);
        Vector2 velocity = IsPlayerActive
            ? direction * speed
            : Vector2.zero;

        rb2d.velocity = velocity;
    }
}
