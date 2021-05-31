using UnityEngine;

public class Obstacle : MonoBehaviour, IDamageDealer
{
    private readonly int damage = 1;

    public int GetDamage() => damage;
}
