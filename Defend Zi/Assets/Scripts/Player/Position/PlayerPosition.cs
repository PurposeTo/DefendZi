using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPosition : MonoBehaviour, IPosition
{
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    public void MoveTo(Vector2 vector)
    {
        rb2d.MovePosition(vector);
    }

    public Vector2 GetPosition() => rb2d.position;
}
