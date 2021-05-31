using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private GameOverView gameOverView;

    private Player player;

    private void Awake()
    {
        GameObjectsHolder.OnInited += (gameObjectsHolder) =>
        {
            InitDataModels(gameObjectsHolder);
            InitViews();

            GameManager.OnInited += (gameManager) =>
            {
                SubscribeEvents(gameManager);
            };
        };
    }

    private void OnDestroy()
    {
        UnsubscribeEvents(GameManager.Instance);
    }

    private void SubscribeEvents(GameManager gameManager)
    {
        player.OnDied += gameOverView.Enable;
        gameOverView.OnReloadLvlClicked += gameManager.ReloadLvl;
    }

    private void UnsubscribeEvents(GameManager gameManager)
    {
        player.OnDied -= gameOverView.Enable;
        gameOverView.OnReloadLvlClicked -= gameManager.ReloadLvl;
    }

    private void InitDataModels(GameObjectsHolder gameObjectsHolder)
    {
        player = gameObjectsHolder.Player;
    }

    private void InitViews()
    {
        gameOverView.Enable(); // включить UI для первоначальной отрисовки и кеширования отрендеренных данных
        gameOverView.Disable();
    }

}
