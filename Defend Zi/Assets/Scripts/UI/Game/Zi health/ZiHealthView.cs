using UnityEngine;
using TMPro;

public class ZiHealthView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text health;

    public void ShowHealth(int value)
    {
        health.text = $"health: {value}";
    }
}
