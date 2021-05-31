using UnityEngine;

[RequireComponent(typeof(Collider2D))] //��������� ����� ���������� ����� ������� �����������
public class Obstacle : MonoBehaviour, IDamageDealer
{
    private readonly uint damage = 1;

    public uint GetDamage() => damage;
}
