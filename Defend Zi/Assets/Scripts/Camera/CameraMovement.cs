using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    private IPositionAccessor playerPosition;
    private float offsetOx;

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
        Vector3 current = transform.position;
        Vector3 target = new Vector3(playerPosition.Value.x + offsetOx, current.y, current.z);
        transform.position = target;
    }
}
