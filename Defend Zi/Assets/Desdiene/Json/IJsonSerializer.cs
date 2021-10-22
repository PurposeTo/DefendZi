namespace Desdiene.Json
{
   public interface IJsonSerializer<T>
    {
        string ToJson(T data);
    }
}
