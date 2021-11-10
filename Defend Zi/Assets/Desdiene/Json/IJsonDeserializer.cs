namespace Desdiene.Json
{
    public interface IJsonDeserializer<T>
    {
        T ToObject(string json);
    }
}
