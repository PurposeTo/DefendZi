using System;
using UnityEngine;

[Serializable]
public class PlayerMovementData
{
    [SerializeField] public float speed = 12f;
    [SerializeField] public float amplitude = 6f;
    [SerializeField] public float defaultFrequency = 0.1f;
    [SerializeField] public float controlledFrequency = 0.5f;
    [SerializeField] public float frequencyChangeRate = 1.5f;
}
