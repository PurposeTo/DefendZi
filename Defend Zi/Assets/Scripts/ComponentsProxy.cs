using Desdiene.Singleton.Unity;
using UnityEngine;

public class ComponentsProxy : SceneSingleton<ComponentsProxy>
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerPosition playerPosition;
    public IDeath PlayerDeath => playerHealth;
    public IPosition PlayerPosition => playerPosition;
}
