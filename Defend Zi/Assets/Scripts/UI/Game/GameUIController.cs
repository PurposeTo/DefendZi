using Desdiene.Types.AtomicReference.Api;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private ZiHealthView healthView;
    private IReadRef<int> health;

    [SerializeField]
    private PlayerScoreView scoreView;
    private IReadRef<int> score;

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
        score.OnValueChanged += UpdateScoreView;
        health.OnValueChanged += UpdateHealthView;
        gameManager.OnGameOver += gameOverView.Enable;
        gameManager.OnGameOver += healthView.Disable;
        gameOverView.OnReloadLvlClicked += gameManager.ReloadLvl;
    }

    private void UnsubscribeEvents(GameManager gameManager)
    {
        score.OnValueChanged -= UpdateScoreView;
        health.OnValueChanged -= UpdateHealthView;
        gameManager.OnGameOver -= gameOverView.Enable;
        gameManager.OnGameOver -= healthView.Disable;
        gameOverView.OnReloadLvlClicked -= gameManager.ReloadLvl;
    }

    private void InitDataModels(GameObjectsHolder gameObjectsHolder)
    {
        score = gameObjectsHolder.PlayerPresenter.Score;
        health = gameObjectsHolder.ZiPresenter.Health.GetHealth();
    }

    private void InitViews()
    {
        healthView.Enable();
        UpdateHealthView();
        scoreView.Enable();
        UpdateScoreView();
        gameOverView.Disable();
    }

    private void UpdateHealthView() => healthView.ShowHealth(health.Get());

    private void UpdateScoreView() => scoreView.ShowScore(score.Get());
}
