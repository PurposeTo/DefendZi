using UnityEngine;

public class ZiAura : MonoBehaviour
{
    private ZiHealth ziHealth;

    private PlayerAura playerAura;

    private void Awake()
    {
        ziHealth = GetComponent<ZiHealth>();
    }


    private void OnDisable()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerAura playerAura))
        {
            EnableCharge(playerAura);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerAura playerAura))
        {
            DisableCharge(playerAura);
        }
    }


    private void EnableCharge(PlayerAura playerAura)
    {
        this.playerAura = playerAura;
        DisableCharge(playerAura);
    }

    private void DisableCharge(PlayerAura playerAura)
    {
        playerAura.DisableCharging();
        playerAura = null;
    }
}
