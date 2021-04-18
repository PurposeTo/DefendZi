using UnityEngine;
using Desdiene.Singleton;

public class GameObjectsHolder : SingletonSuperMonoBehaviour<GameObjectsHolder>
{
    [SerializeField]
    private Zi zi;
    public Zi Zi => zi;

    [SerializeField]
    private PlayerPresenter playerPresenter;
    public PlayerPresenter PlayerPresenter => playerPresenter;
}
