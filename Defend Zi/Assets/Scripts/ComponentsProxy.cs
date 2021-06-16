using UnityEngine;

public class ComponentsProxy : MonoBehaviour
{
    [SerializeField, NotNull] private PlayerMono player;
    [SerializeField, NotNull] private CameraSize cameraSize;
    public IDeath PlayerDeath => player;
    public IPositionGetter PlayerPosition => player;
    public IPositionNotification PlayerPositionNotification => player;
    public IScoreGetter PlayerScore => player;
    public IScoreNotification PlayerScoreNotification => player;
    public CameraSize CameraSize => cameraSize;
}
