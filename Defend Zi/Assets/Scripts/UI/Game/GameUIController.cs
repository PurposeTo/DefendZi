using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private GameOverView gameOverView;

    //TODO: заменить на интерфейс с одним лишь событием!
    private IHealth<int> playerHealth;

    private void Awake()
    {
        PlayerHealth.OnInited += (playerHealth) =>
        {
            InitDataModels(playerHealth);
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
        playerHealth.OnDied += gameOverView.Enable;
        gameOverView.OnReloadLvlClicked += gameManager.ReloadLvl;
    }

    private void UnsubscribeEvents(GameManager gameManager)
    {
        playerHealth.OnDied -= gameOverView.Enable;
        gameOverView.OnReloadLvlClicked -= gameManager.ReloadLvl;
    }

    private void InitDataModels(PlayerHealth playerHealth)
    {
        this.playerHealth = playerHealth;
    }

    private void InitViews()
    {
        gameOverView.Enable(); // включить UI для первоначальной отрисовки и кеширования отрендеренных данных
        gameOverView.Disable();
    }

}
