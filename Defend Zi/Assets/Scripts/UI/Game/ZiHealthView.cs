using UnityEngine;
using TMPro;

public class ZiHealthView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text health;

    public void Enable() => health.gameObject.SetActive(true);

    public void Disable() => health.gameObject.SetActive(false);

    public void ShowHealth(int value)
    {
        health.text = $"health: {value}";
    }
}
