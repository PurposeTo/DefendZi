using Desdiene.Types.Rectangles;
using UnityEngine;

public class ComponentsProxy : MonoBehaviour
{
    [SerializeField, NotNull] private PlayerMono _player;
    [SerializeField, NotNull] private GameSpaceInSight _visibleGameSpace;
    public IDeath PlayerDeath => _player;
    public IPositionAccessor PlayerPosition => _player;
    public IPositionNotification PlayerPositionNotification => _player;
    public IScoreAccessor PlayerScore => _player;
    public IScoreNotification PlayerScoreNotification => _player;
    public IRectangleIn2DAccessor VisibleGameSpace => _visibleGameSpace;
}
