using UnityEngine;

public class ComponentsProxy : MonoBehaviour
{
    [SerializeField] private Player player;
    public IDeath PlayerDeath => player;
    public IPositionGetter PlayerPosition => player;
}
