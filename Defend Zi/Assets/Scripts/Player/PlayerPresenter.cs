using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerActivity))]
[RequireComponent(typeof(PlayerAttack))]
public class PlayerPresenter : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerActivity playerActivity;
    private PlayerAttack playerAttack;


    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerActivity = GetComponent<PlayerActivity>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    public PlayerMovement GetPlayerMovement()
    {
        if(playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
        return playerMovement;
    }

    public PlayerActivity GetPlayerActivity()
    {
        if (playerMovement == null) playerActivity = GetComponent<PlayerActivity>();
        return playerActivity;
    }


    public PlayerAttack GetPlayerAttack()
    {
        if (playerAttack == null) playerAttack = GetComponent<PlayerAttack>();
        return playerAttack;
    }
}
