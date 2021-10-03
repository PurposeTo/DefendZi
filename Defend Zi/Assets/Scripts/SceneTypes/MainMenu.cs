using Desdiene.MonoBehaviourExtension;
using Desdiene.UnityScenes;

namespace SceneTypes
{
    public static class MainMenu
    {
        public static SceneAsset Get(MonoBehaviourExt mono, ScenesInBuild scenesInBuild)
        {
           return scenesInBuild.Get(mono, nameof(MainMenu));
        }
    }
}
