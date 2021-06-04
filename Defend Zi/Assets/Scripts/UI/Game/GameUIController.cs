using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private GameOverView gameOverView;

    private IDeath playerDeath;

    private void Awake()
    {
        ComponentsProxy.OnInited += (componentsProxy) =>
        {
            InitDataModels(componentsProxy);
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
        playerDeath.OnDied += gameOverView.Enable;
        gameOverView.OnReloadLvlClicked += gameManager.ReloadLvl;
    }

    private void UnsubscribeEvents(GameManager gameManager)
    {
        playerDeath.OnDied -= gameOverView.Enable;
        gameOverView.OnReloadLvlClicked -= gameManager.ReloadLvl;
    }

    private void InitDataModels(ComponentsProxy componentsProxy)
    {
        playerDeath = componentsProxy.PlayerDeath;
    }

    private void InitViews()
    {
        gameOverView.Enable(); // включить UI для первоначальной отрисовки и кеширования отрендеренных данных
        gameOverView.Disable();
    }

}
