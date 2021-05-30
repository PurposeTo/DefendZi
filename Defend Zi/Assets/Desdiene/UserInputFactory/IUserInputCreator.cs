using UnityEngine;

namespace Desdiene.UserInputFactory
{
    public interface IUserInputCreator<T>
    {
        public T GetOrDefault();

        public T GetOrDefault(RuntimePlatform platform);
    }
}
