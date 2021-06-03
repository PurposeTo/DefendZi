using Desdiene.Singleton;
using UnityEngine;

public class GameObjectsHolder : SingletonMonoBehaviourExt<GameObjectsHolder>
{
    [SerializeField] private Player player;
    public Player Player => player;
}
