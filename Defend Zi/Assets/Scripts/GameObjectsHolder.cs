using UnityEngine;
using Desdiene.Singleton;

public class GameObjectsHolder : SingletonSuperMonoBehaviour<GameObjectsHolder>
{
    [SerializeField] private Player player;
    public Player Player
    {
        get
        {
            return player;
        }
    }
}
