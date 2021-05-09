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
        GameObjectsHolder.InitializedInstance += (instance) =>
        {
            instance.ZiPresenter.Health.OnZiDie += EnableGameOverScreen;
        };
    }

    private void UnsubscribeEvents()
    {
        GameObjectsHolder.Instance.ZiPresenter.Health.OnZiDie -= EnableGameOverScreen;
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
