using Desdiene.Types.AtomicReference;
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
        GameObjectsHolder.InitializedInstance += (_) =>
        {
            InitDataModels();
            InitViews();

            GameManager.InitializedInstance += (_2) =>
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
        GameManager instance = GameManager.Instance;
        score.OnValueChanged += UpdateScoreView;
        health.OnValueChanged += UpdateHealthView;
        instance.OnGameOver += gameOverView.Enable;
        instance.OnGameOver += healthView.Disable;
        gameOverView.OnReloadLvlClicked += instance.ReloadLvl;
    }

    private void UnsubscribeEvents()
    {
        GameManager instance = GameManager.Instance;
        score.OnValueChanged -= UpdateScoreView;
        health.OnValueChanged -= UpdateHealthView;
        instance.OnGameOver -= gameOverView.Enable;
        instance.OnGameOver -= healthView.Disable;
        gameOverView.OnReloadLvlClicked -= instance.ReloadLvl;
    }

    private void InitDataModels()
    {
        GameObjectsHolder instance = GameObjectsHolder.Instance;
        score = instance.PlayerPresenter.Score;
        health = instance.ZiPresenter.Health.GetHealth();
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
