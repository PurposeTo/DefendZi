using UnityEngine;
using Desdiene.Singleton;

public class GameObjectsHolder : SingletonSuperMonoBehaviour<GameObjectsHolder>
{
    [SerializeField]
    private ZiPresenter ziPresenter;
    public ZiPresenter ZiPresenter => ziPresenter;

    [SerializeField]
    private PlayerPresenter playerPresenter;
    public PlayerPresenter PlayerPresenter => playerPresenter;
}
