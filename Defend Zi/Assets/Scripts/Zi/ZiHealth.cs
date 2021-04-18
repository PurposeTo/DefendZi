using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ZiHealth : MonoBehaviour
{
    private int health = 3;
    
    
    public void TakeDamage()
    {
        health -= 1;
        if (health < 0) health = 0;
    }
}
