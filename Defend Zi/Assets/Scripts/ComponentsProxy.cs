using UnityEngine;

public class ComponentsProxy : MonoBehaviour
{
    [SerializeField] private PlayerMono player;
    public IDeath PlayerDeath => player;
    public IPositionGetter PlayerPosition => player;
}
