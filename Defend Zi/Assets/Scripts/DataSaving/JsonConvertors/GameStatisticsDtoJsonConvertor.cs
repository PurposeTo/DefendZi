using Desdiene.Json;
using Newtonsoft.Json;

public class GameStatisticsDtoJsonConvertor : IJsonConvertor<GameStatisticsDto>
{
    private readonly IJsonConvertor<GameStatisticsDto> _jsonConvertor;

    public GameStatisticsDtoJsonConvertor()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        _jsonConvertor = new NewtonsoftJsonConvertor<GameStatisticsDto>(settings);
    }

    public string ToJson(GameStatisticsDto data) => _jsonConvertor.ToJson(data);

    public GameStatisticsDto ToObject(string json) => _jsonConvertor.ToObject(json);
}
