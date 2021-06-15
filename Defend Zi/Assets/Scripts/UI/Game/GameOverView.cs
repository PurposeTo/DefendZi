using System;
using UnityEngine;

public class GameOverView : MonoBehaviour
{
    [SerializeField, NotNull]
    private GameObject gameOverScreen;

    public event Action OnReloadLvlClicked;

    public void Enable()
    {
        gameOverScreen.SetActive(true);
    }

    public void Disable()
    {
        gameOverScreen.SetActive(false);
    }

    public void ReloadLvlButton()
    {
        OnReloadLvlClicked?.Invoke();
    }
}
