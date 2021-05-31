using UnityEngine;
using Desdiene.Singleton;

public class GameObjectsHolder : SingletonMonoBehaviourExt<GameObjectsHolder>
{
    [SerializeField] private Player player;
    public Player Player => player;
}
