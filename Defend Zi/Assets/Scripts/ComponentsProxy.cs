using Desdiene.Types.Rectangles;
using UnityEngine;

public class ComponentsProxy : MonoBehaviour
{
    [SerializeField, NotNull] private PlayerMono _player;
    [SerializeField, NotNull] private PlayerLifeTime _playerLifeTime;
    [SerializeField, NotNull] private GameSpaceInSight _visibleGameSpace;
    public IHealthNotification PlayerDeath => _player;
    public IReincarnation PlayerReincarnation => _player;
    public IPositionAccessor PlayerPosition => _player;
    public IPositionNotifier PlayerPositionNotification => _player;
    public IScoreAccessor PlayerScore => _player;
    public IScoreNotification PlayerScoreNotification => _player;
    public PlayerLifeTime PlayerLifeTime => _playerLifeTime;
    public IRectangleIn2DAccessor VisibleGameSpace => _visibleGameSpace;
}
