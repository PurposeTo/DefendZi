using UnityEngine;

public class PlayerMovementView
{
    private readonly GameDifficulty _gameDifficulty;
    private readonly PlayerMovementData _playerMovementData;

    public PlayerMovementView(GameDifficulty gameDifficulty, PlayerMovementData playerMovementData)
    {
        _gameDifficulty = gameDifficulty ?? throw new System.ArgumentNullException(nameof(gameDifficulty));
        _playerMovementData = playerMovementData ?? throw new System.ArgumentNullException(nameof(playerMovementData));
    }

    public float Speed => Mathf.Lerp(_playerMovementData.Speed.Min, _playerMovementData.Speed.Max, _gameDifficulty.Get());
    public float Amplitude => _playerMovementData.Amplitude;
    public float DefaultFrequency => _playerMovementData.DefaultFrequency;
    public float ControlledFrequency => _playerMovementData.ControlledFrequency;
    public float FrequencyChangeRate => _playerMovementData.FrequencyChangeRate;
}
