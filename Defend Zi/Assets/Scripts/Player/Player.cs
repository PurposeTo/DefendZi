using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    private PlayerMovement movement;

    private void Awake()
    {
        movement = gameObject.GetComponent<PlayerMovement>();
    }
}
