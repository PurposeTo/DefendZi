namespace Desdiene.Json
{
    public interface IJsonConvertor<T>
    {
        T Deserialize(string jsonData);

        string Serialize(T data);
    }
}
