using Desdiene.Types.RectangleAsset;
using UnityEngine;

public class ComponentsProxy : MonoBehaviour
{
    [SerializeField, NotNull] private PlayerMono _player;
    [SerializeField, NotNull] private GameSpaceInSight _visibleGameSpace;
    public IDeath PlayerDeath => _player;
    public IPositionGetter PlayerPosition => _player;
    public IPositionNotification PlayerPositionNotification => _player;
    public IScoreGetter PlayerScore => _player;
    public IScoreNotification PlayerScoreNotification => _player;
    public IRectangleIn2DGetter VisibleGameSpace => _visibleGameSpace;
}
