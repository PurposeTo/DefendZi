using UnityEngine;

public class ComponentsProxy : MonoBehaviour
{
    [SerializeField, NotNull] private PlayerMono _player;
    public IDeath PlayerDeath => _player;
    public IPositionGetter PlayerPosition => _player;
    public IPositionNotification PlayerPositionNotification => _player;
    public IScoreGetter PlayerScore => _player;
    public IScoreNotification PlayerScoreNotification => _player;
}
