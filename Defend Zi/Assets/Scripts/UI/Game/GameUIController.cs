using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private ZiHealthView healthView;
    private IStat<int> health;

    [SerializeField]
    private PlayerScoreView scoreView;
    private IStat<int> score;

    [SerializeField]
    private GameOverView gameOverView;

    private void Awake()
    {
        GameObjectsHolder.InitializedInstance += (instance) =>
        {
            score = instance.PlayerPresenter.Score;
            health = instance.ZiPresenter.Health.GetHealth();
            InitViews();

            GameManager.InitializedInstance += (_) =>
            {
                SubscribeEvents();
            };
        };
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        score.OnValueChanged += UpdateScoreView;
        health.OnValueChanged += UpdateHealthView;
        GameManager.Instance.OnGameOver += gameOverView.EnableScreen;
        GameManager.Instance.OnGameOver += healthView.DisableHealthView;
        gameOverView.OnReloadLvlClicked += ReloadLvl;
    }

    private void UnsubscribeEvents()
    {
        score.OnValueChanged -= UpdateScoreView;
        health.OnValueChanged -= UpdateHealthView;
        GameManager.Instance.OnGameOver -= gameOverView.EnableScreen;
        GameManager.Instance.OnGameOver -= healthView.DisableHealthView;
        gameOverView.OnReloadLvlClicked -= ReloadLvl;
    }


    private void InitViews()
    {
        healthView.EnableHealthView();
        UpdateHealthView();
        scoreView.EnableScoreView();
        UpdateScoreView();
        gameOverView.DisableScreen();
    }


    private void UpdateHealthView() => healthView.ShowHealth(health.Value);

    private void UpdateScoreView() => scoreView.ShowScore(score.Value);

    private void ReloadLvl() => GameManager.Instance.ReloadLvl();
}
