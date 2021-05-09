using UnityEngine;

public class GameOverView : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverScreen;

    public void EnableScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void DisableScreen()
    {
        gameOverScreen.SetActive(false);
    }

    public void PrintGameOver()
    {
        Debug.Log("Game over!");
    }
}
