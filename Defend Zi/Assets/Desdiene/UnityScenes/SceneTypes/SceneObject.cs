using UnityEngine.SceneManagement;

namespace Desdiene.UnityScenes.SceneTypes
{
    /// <summary>
    /// Описывает сцену-объект в окне иерархии объектов.
    /// </summary>
    public struct SceneObject
    {
        private Scene _unityScene;

        public SceneObject(Scene unityScene)
        {
            _unityScene = unityScene;
        }

        public string Name => _unityScene.name;
        public bool IsLoadedAndEnabled => _unityScene.isLoaded;
        public Scene UnityScene => _unityScene;

        public override bool Equals(object obj) => _unityScene.Equals(obj);
        public override int GetHashCode() => _unityScene.GetHashCode();
        public static bool operator ==(SceneObject lhs, SceneObject rhs) => lhs._unityScene == rhs._unityScene;
        public static bool operator !=(SceneObject lhs, SceneObject rhs) => lhs._unityScene != rhs._unityScene;
    }
}
