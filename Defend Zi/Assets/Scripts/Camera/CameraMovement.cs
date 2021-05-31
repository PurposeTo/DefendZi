using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float offsetOx;
    private Player player;

    private void Awake()
    {
        GameObjectsHolder.OnInited += (gameObjectsHolder) => player = gameObjectsHolder.Player;

        offsetOx = transform.position.x - player.transform.position.x;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 playerPosition = player.transform.position;
        transform.position = new Vector3(playerPosition.x + offsetOx, transform.position.y, transform.position.z);
    }
}
