using System;

namespace Desdiene.Json
{
    [Obsolete]
    public interface IJsonConvertor<T>
    {
        T Deserialize(string jsonData);

        string Serialize(T data);
    }
}
