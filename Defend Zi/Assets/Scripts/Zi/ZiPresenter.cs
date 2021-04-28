using UnityEngine;

[RequireComponent(typeof(Zi))]
public class ZiPresenter : MonoBehaviour
{
    private Zi zi;
    private ZiHealth ziHealth;
    private ZiAura ziAura;

    private void Awake()
    {
        zi = GetZi();
        ziHealth = GetZiHealth();
        ziAura = GetZiAura();
    }

    public Zi GetZi()
    {
        if (zi == null) zi = GetComponent<Zi>();
        return zi;
    }

    public ZiHealth GetZiHealth()
    {
        if (ziHealth == null) ziHealth = GetComponentInChildren<ZiHealth>();
        return ziHealth;
    }

    public ZiAura GetZiAura()
    {
        if (ziAura == null) ziAura = GetComponentInChildren<ZiAura>();
        return ziAura;
    }
}
