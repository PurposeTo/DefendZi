using Desdiene.TimeControl.Pause;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private PlayerMovement PlayerMovement => GameObjectsHolder.Instance.PlayerPresenter.Movement;
    private Zi Zi => GameObjectsHolder.Instance.ZiPresenter.Zi;


    private void Update()
    {
        if (!GlobalPauser.Instance.IsPause)
        {
            //Debug.Log($"���������� ������ �� �� � ���������: {PlayerMovement.ZiPlayerDistance.GetPercent()}");
        }
        transform.position = GetNewPosition();
    }

    private Vector3 GetNewPosition()
    {
        Vector3 position = Vector3.Lerp(
            Zi.transform.position,
            PlayerMovement.transform.position,
            PlayerMovement.ZiPlayerDistance.GetPercent())
            / 2f;
        return new Vector3(position.x, position.y, transform.position.z);
    }
}
