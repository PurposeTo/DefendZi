using Desdiene.Singleton.Unity;
using UnityEngine;

public class ComponentsProxy : SceneSingleton<ComponentsProxy>
{
    [SerializeField] private Player player;
    public IDeath PlayerDeath => player;
    public IPositionGetter PlayerPosition => player;
}
