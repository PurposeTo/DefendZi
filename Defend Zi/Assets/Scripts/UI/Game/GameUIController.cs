using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private GameOverView gameOverView;

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
        gameOverView.OnReloadLvlClicked += gameManager.ReloadLvl;
    }

    private void UnsubscribeEvents(GameManager gameManager)
    {
        gameOverView.OnReloadLvlClicked -= gameManager.ReloadLvl;
    }

    private void InitDataModels(GameObjectsHolder gameObjectsHolder)
    {

    }

    private void InitViews()
    {

    }

}
