using UnityEngine;

namespace Desdiene.ControllerFactory
{
    public interface IUserControllerCreator<T>
    {
        public T GetOrDefault();

        public T GetOrDefault(RuntimePlatform platform);
    }
}
