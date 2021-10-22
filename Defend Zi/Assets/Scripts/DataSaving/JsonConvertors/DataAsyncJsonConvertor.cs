using Desdiene.Json;
using Newtonsoft.Json;

public class DataAsyncJsonConvertor : IJsonConvertor<SavableDataAsync>
{
    private readonly IJsonConvertor<SavableDataAsync> _jsonConvertor;

    public DataAsyncJsonConvertor()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        _jsonConvertor = new NewtonsoftJsonConvertor<SavableDataAsync>(settings);
    }

    public string ToJson(SavableDataAsync data) => _jsonConvertor.ToJson(data);

    public SavableDataAsync ToObject(string json) => _jsonConvertor.ToObject(json);
}
