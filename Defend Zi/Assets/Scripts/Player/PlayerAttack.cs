using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerAttack : MonoBehaviour
{
    private PlayerScore playerScore;

    public PlayerAttack Constructor(PlayerScore playerScore)
    {
        this.playerScore = playerScore;
        return this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage();
            playerScore.AddScore(1);
        }
    }
}
