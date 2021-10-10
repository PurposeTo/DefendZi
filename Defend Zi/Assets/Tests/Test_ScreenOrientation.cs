using UnityEngine;
using TMPro;

public class OrientationTest : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Update()
    {
        _text.text = Screen.orientation.ToString();
    }
}
