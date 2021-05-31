using UnityEngine;

[RequireComponent(typeof(Collider2D))] //Нанесение урона происходит через триггер коллайдеров
public class Obstacle : MonoBehaviour, IDamageDealer
{
    private readonly int damage = 1;

    public int GetDamage() => damage;
}
