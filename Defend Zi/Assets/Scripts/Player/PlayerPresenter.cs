using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerActivity))]
public class PlayerPresenter : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerActivity playerActivity;
    private PlayerAttack playerAttack;
    private PlayerAura playerAura;


    private void Awake()
    {
        playerMovement = GetPlayerMovement();
        playerActivity = GetPlayerActivity();
        playerAttack = GetPlayerAttack();
        playerAura = GetPlayerAura();
    }

    public PlayerMovement GetPlayerMovement()
    {
        if(playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
        return playerMovement;
    }

    public PlayerActivity GetPlayerActivity()
    {
        if (playerActivity == null) playerActivity = GetComponent<PlayerActivity>();
        return playerActivity;
    }

    public PlayerAttack GetPlayerAttack()
    {
        if (playerAttack == null) playerAttack = GetComponentInChildren<PlayerAttack>();
        return playerAttack;
    }

    public PlayerAura GetPlayerAura()
    {
        if (playerAura == null) playerAura = GetComponentInChildren<PlayerAura>();
        return playerAura;
    }
}
