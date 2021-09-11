using UnityEngine;

namespace Desdiene.UserInputFactories
{
    public interface IUserInputFactory<T>
    {
        public T GetOrDefault();

        public T GetOrDefault(RuntimePlatform platform);
    }
}
