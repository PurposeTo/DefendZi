using System;
using UnityEngine;

public class GameOverView : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverScreen;

    public event Action OnReloadLvlClicked;

    public void EnableScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void DisableScreen()
    {
        gameOverScreen.SetActive(false);
    }

    public void ReloadLvlButton()
    {
        OnReloadLvlClicked?.Invoke();
    }
}
