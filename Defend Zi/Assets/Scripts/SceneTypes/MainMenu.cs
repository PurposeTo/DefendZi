using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.UnityScenes;
using Desdiene.UnityScenes.Types;

namespace SceneTypes
{
    public static class MainMenu
    {
        public static ISceneAsset Get(MonoBehaviourExt mono, ScenesInBuild scenesInBuild)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));
            if (scenesInBuild == null) throw new ArgumentNullException(nameof(scenesInBuild));

            return scenesInBuild.Get(mono, nameof(MainMenu));
        }
    }
}
