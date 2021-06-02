using UnityEngine;

public class PlayerScore : MonoBehaviour, IScoreCollector
{
    public int Value { get; private set; }

    public void Add(int amount)
    {
        Value += amount;
    }
}
