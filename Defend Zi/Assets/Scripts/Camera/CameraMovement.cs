using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Player player;


    private void Awake()
    {
        GameObjectsHolder.OnInited += (gameObjectsHolder) => player = gameObjectsHolder.Player;
    }


    private void Update()
    {
        Move();
    }


    private void Move()
    {
        Vector3 position = player.transform.position;
        transform.position = new Vector3(position.x, transform.position.y, transform.position.z);
    }
}
