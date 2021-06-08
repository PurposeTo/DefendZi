using UnityEngine;
using Zenject;

public class GameUIController : MonoBehaviour
{
    private GameManager gameManager;

    [Inject]
    private void Constructor(GameManager gameManager)
    {
        this.gameManager = gameManager;

        ComponentsProxy.OnInited += (componentsProxy) =>
        {
            InitDataModels(componentsProxy);
            InitViews();

            SubscribeEvents();
        };
    }

    [SerializeField]
    private GameOverView gameOverView;

    private IDeath playerDeath;


    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        playerDeath.OnDied += gameOverView.Enable;
        gameOverView.OnReloadLvlClicked += gameManager.ReloadLvl;
    }

    private void UnsubscribeEvents()
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
