using Desdiene.Singleton.Unity;
using UnityEngine;

public class GameObjectsHolder : SceneSingleton<GameObjectsHolder>
{
    [SerializeField] private Player player;
    public Player Player => player;
}
