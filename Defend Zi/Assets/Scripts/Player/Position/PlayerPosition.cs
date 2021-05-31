using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPosition : MonoBehaviour, IPosition
{
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    Vector2 IPosition.GetPosition() => rb2d.position;

    void IPosition.MoveTo(Vector2 vector)
    {
        rb2d.MovePosition(vector);
    }
}
