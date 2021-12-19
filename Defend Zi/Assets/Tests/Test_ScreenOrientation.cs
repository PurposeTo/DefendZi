using Desdiene.MonoBehaviourExtension;
using TMPro;
using UnityEngine;

public class Test_ScreenOrientation : MonoBehaviourExt
{
    [SerializeField] private TMP_Text _text;
    private Camera _camera;

    protected override void AwakeExt()
    {
        _camera = Camera.main;
    }

    protected override void UpdateExt()
    {
        string orientationType = Screen.orientation.ToString();
        string width = _camera.pixelWidth.ToString();
        string hight = _camera.pixelHeight.ToString();
        _text.text = $"{orientationType}\n{width}x{hight}";
    }
}
