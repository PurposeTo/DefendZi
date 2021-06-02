using UnityEngine;

[RequireComponent(typeof(Collider2D))] // Нанесение урона происходит через триггер коллайдеров
[RequireComponent(typeof(CircleCollider2D))] // Начисление очков за близкое огибание препятствий происходит через триггер коллайдеров
[RequireComponent(typeof(ScorePoints))]
public class Obstacle : MonoBehaviour, IDamageDealer
{
    private readonly uint damage = 1;

    public uint GetDamage() => damage;
}
