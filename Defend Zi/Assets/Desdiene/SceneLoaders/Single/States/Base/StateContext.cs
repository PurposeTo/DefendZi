using Desdiene.SceneTypes;

namespace Desdiene.SceneLoaders.Single.States.Base
{
    public class StateContext
    {
        public StateContext(SceneAsset scene)
        {
            Scene = scene;
        }

        public SceneAsset Scene { get; }
    }
}
