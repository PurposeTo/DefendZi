using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    private float offsetOx;
    private IPositionGetter playerPosition;

    [Inject]
    private void Constructor(ComponentsProxy componentsProxy)
    {
        playerPosition = componentsProxy.PlayerPosition;
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
