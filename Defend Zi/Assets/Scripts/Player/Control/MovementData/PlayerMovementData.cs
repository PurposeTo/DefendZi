using Desdiene.Types.Percents;
using UnityEngine;

public class PlayerMovementData
{
    private readonly IPercentAccessor _gameDifficulty;
    private readonly PlayerMovementDataMono _playerMovementData;

    public PlayerMovementData(GameDifficulty gameDifficulty, PlayerMovementDataMono playerMovementData)
    {
        _gameDifficulty = gameDifficulty ?? throw new System.ArgumentNullException(nameof(gameDifficulty));
        _playerMovementData = playerMovementData ?? throw new System.ArgumentNullException(nameof(playerMovementData));
    }

    public float Speed => Mathf.Lerp(_playerMovementData.Speed.Min, _playerMovementData.Speed.Max, _gameDifficulty.Value);
    public float Amplitude => _playerMovementData.Amplitude;
    public float DefaultFrequency => _playerMovementData.DefaultFrequency;
    public float ControlledFrequency => _playerMovementData.ControlledFrequency;
    public float FrequencyChangeRate => _playerMovementData.FrequencyChangeRate;
}
