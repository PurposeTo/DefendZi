using Desdiene.Json;
using Newtonsoft.Json;

public class GameSettingsDtoJsonConvertor : IJsonConvertor<GameSettingsDto>
{
    private readonly IJsonConvertor<GameSettingsDto> _jsonConvertor;

    public GameSettingsDtoJsonConvertor()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        _jsonConvertor = new NewtonsoftJsonConvertor<GameSettingsDto>(settings);
    }

    public string ToJson(GameSettingsDto data) => _jsonConvertor.ToJson(data);

    public GameSettingsDto ToObject(string json) => _jsonConvertor.ToObject(json);
}
