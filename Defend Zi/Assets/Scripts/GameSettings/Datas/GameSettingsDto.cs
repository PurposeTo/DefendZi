using System;
using Desdiene.DataSaving.Datas;
using Desdiene.Json;

/* 
 * Для (де)сериализации используется NewtonsoftJson, 
 * поэтому все данные должны лежать в свойствах и иметь public get и public set, а также быть проинициализированны.
 */
public class GameSettingsDto : IValidData, IJsonSerializable
{
    bool IValidData.IsValid() => IsValid();

    void IValidData.TryToRepair()
    {
        if (!IsValid()) Repair();
    }

    string IJsonSerializable.ToJson()
    {
        IJsonSerializer<GameSettingsDto> jsonSerializer = new GameSettingsDtoJsonConvertor();
        return jsonSerializer.ToJson(this);
    }

    public bool SoundEnabled { get; set; }

    public override string ToString()
    {
        return $"{GetType().Name}"
            + $"\nSoundEnabled={SoundEnabled}";
    }

    private bool IsValid()
    {
        // сейчас нельзя сломать данные, т.к. нет nullable полей.
        return true;
    }

    private void Repair()
    {
        // сейчас нельзя сломать данные, т.к. нет nullable полей.
    }
}
