using UnityEngine;
using TMPro;
using Desdiene.MonoBehaviourExtension;

public class Test_ScreenOrientation : MonoBehaviourExt
{
    [SerializeField] private TMP_Text _text;
    private Camera _camera;

    protected override void AwakeExt()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        string orientationType = Screen.orientation.ToString();
        string width = _camera.pixelWidth.ToString();
        string hight = _camera.pixelHeight.ToString();
        _text.text = $"{orientationType}\n{width}x{hight}";
    }
}
