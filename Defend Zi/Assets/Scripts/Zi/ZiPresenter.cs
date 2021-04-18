using UnityEngine;

[RequireComponent(typeof(ZiHealth))]
[RequireComponent(typeof(Zi))]
public class ZiPresenter : MonoBehaviour
{
    private ZiHealth ziHealth;
    private Zi zi;

    private void Awake()
    {
        ziHealth = GetComponent<ZiHealth>();
        zi = GetComponent<Zi>();
    }

    public ZiHealth GetZiHealth()
    {
        if (ziHealth == null) ziHealth = GetComponent<ZiHealth>();
        return ziHealth;
    }

    public Zi GetZi()
    {
        if (zi == null) zi = GetComponent<Zi>();
        return zi;
    }
}
