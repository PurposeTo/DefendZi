using UnityEngine;
using TMPro;

public class ZiHealthView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text health;

    public void SetHealth(int value)
    {
        health.text = $"health: {value}";
    }
}
