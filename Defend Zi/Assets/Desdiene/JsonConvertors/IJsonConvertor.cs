namespace Desdiene.JsonConvertorWrapper
{
    public interface IJsonConvertor<T>
    {
        T Deserialize(string jsonData);

        string Serialize(T data);
    }
}
