using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        GameObjectsHolder.OnInited += (gameObjectsHolder) => player = gameObjectsHolder.Player;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 playerPosition = player.transform.position;
        transform.position = new Vector3(playerPosition.x, transform.position.y, transform.position.z);
    }
}
