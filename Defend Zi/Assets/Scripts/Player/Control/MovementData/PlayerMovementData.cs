using System;
using Desdiene.Types.Ranges.Positive;
using UnityEngine;

[Serializable]
public class PlayerMovementData
{
    [SerializeField] private FloatRange _speed = new FloatRange(12f, 18f);
    [SerializeField] private float _amplitude = 6f;
    [SerializeField] private float _defaultFrequency = 0.1f;
    [SerializeField] private float _controlledFrequency = 0.5f;
    [SerializeField] private float _frequencyChangeRate = 1.5f;

    public FloatRange Speed => _speed;
    public float Amplitude => _amplitude;
    public float DefaultFrequency => _defaultFrequency;
    public float ControlledFrequency => _controlledFrequency;
    public float FrequencyChangeRate => _frequencyChangeRate;
}
