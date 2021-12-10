using System;
using Desdiene.DataSaving.Datas;
using Desdiene.Json;

/* 
 * Для (де)сериализации используется NewtonsoftJson, 
 * поэтому все данные должны лежать в свойствах и иметь public get и public set, а также быть проинициализированны.
 */
public class GameSettingsDto : IValidData, IJsonSerializable, ICloneable
{
    bool IValidData.IsValid() => IsValid();

    void IValidData.Repair()
    {
        if (!IsValid()) Repair();
    }

    string IJsonSerializable.ToJson()
    {
        IJsonSerializer<GameSettingsDto> jsonSerializer = new GameSettingsDtoJsonConvertor();
        return jsonSerializer.ToJson(this);
    }

    object ICloneable.Clone()
    {
        return MemberwiseClone();
    }

    public bool SoundMuted { get; set; }

    public override string ToString()
    {
        return $"{GetType().Name}"
            + $"\nSoundMuted={SoundMuted}";
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

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
