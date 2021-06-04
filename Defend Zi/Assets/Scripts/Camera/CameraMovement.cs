using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float offsetOx;
    private IPosition playerPosition;

    private void Awake()
    {
        ComponentsProxy.OnInited += (componentsProxy) => playerPosition = componentsProxy.PlayerPosition;

        offsetOx = transform.position.x - playerPosition.Value.x;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position = new Vector3(playerPosition.Value.x + offsetOx, transform.position.y, transform.position.z);
    }
}
