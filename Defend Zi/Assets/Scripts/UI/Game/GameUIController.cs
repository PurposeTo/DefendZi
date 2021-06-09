using UnityEngine;
using Zenject;

public class GameUIController : MonoBehaviour
{
    private GameManager gameManager;
    private IDeath playerDeath;

    [Inject]
    private void Constructor(GameManager gameManager, ComponentsProxy componentsProxy)
    {
        this.gameManager = gameManager;
        playerDeath = componentsProxy.PlayerDeath;
        InitViews();
        SubscribeEvents();
    }

    [SerializeField]
    private GameOverView gameOverView;



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

    private void InitViews()
    {
        gameOverView.Enable(); // ¬ключить UI дл€ первоначальной отрисовки и кешировани€ отрендеренных данных
        gameOverView.Disable();
    }

}
