using UnityEngine;

[RequireComponent(typeof(GameOverView))]
public class GameOverPresenter : MonoBehaviour
{
    private GameOverView gameOverView;

    private void Awake()
    {
        gameOverView = GetComponent<GameOverView>();
        DisableGameOverScreen();
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        GameManager.InitializedInstance += (instance) =>
        {
            instance.OnGameOver += EnableGameOverScreen;
        };
    }

    private void UnsubscribeEvents()
    {
        GameManager.Instance.OnGameOver -= EnableGameOverScreen;
    }

    private void DisableGameOverScreen()
    {
        gameOverView.DisableScreen();
    }

    private void EnableGameOverScreen()
    {
        gameOverView.EnableScreen();
        gameOverView.PrintGameOver();
    }
}
