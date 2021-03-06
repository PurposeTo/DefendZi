using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

public class CameraOrientationMono : MonoBehaviourExt
{
    [SerializeField, NotNull(IgnorePrefab = true)] private Camera _camera;
    private IScreenOrientation _screenOrientationWrap;
    private CameraOrientation _cameraOrientation;

    [Inject]
    private void Constructor(IScreenOrientation screenOrientation)
    {
        _screenOrientationWrap = screenOrientation != null
            ? screenOrientation
            : throw new ArgumentNullException(nameof(screenOrientation));

    }

    protected override void AwakeExt()
    {
        /* Ошибка компиляции при использовании тернарного оператора
         * error CS0173: Type of conditional expression cannot be determined because there is no implicit conversion between 'OrthographicCameraOrientation' and 'PerspectiveCameraOrientation'
         */
        if (_camera.orthographic)
        {
            _cameraOrientation = new OrthographicCameraOrientation(_screenOrientationWrap, _camera);
        }
        {
            _cameraOrientation = new PerspectiveCameraOrientation(_screenOrientationWrap, _camera);
        }
    }

    protected override void OnDestroyExt()
    {
        _cameraOrientation.Destroy();
    }
}
