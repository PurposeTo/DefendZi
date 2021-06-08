using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementData", menuName = "ScriptableObjects/PlayerMovementData")]
public class PlayerMovementData : ScriptableObject
{
    public float speed = 12f;
    public float amplitude = 6f;
    public float defaultFrequency = 0.15f;
    public float controlledFrequency = 0.5f;
    public float frequencyChangeRate = 1.5f;
}
