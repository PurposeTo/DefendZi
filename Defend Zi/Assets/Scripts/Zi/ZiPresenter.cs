using UnityEngine;
using Desdiene.Types.AtomicReference.RefRuntimeInit;

[RequireComponent(typeof(Zi))]
public class ZiPresenter : MonoBehaviour
{
    public Zi Zi => ziRef.GetOrInit(InitZi);
    public ZiHealth Health => healthRef.GetOrInit(InitZiHealth);
    public ZiAura Aura => auraRef.GetOrInit(InitZiAura);

    private readonly RefRuntimeInit<Zi> ziRef = new RefRuntimeInit<Zi>();
    private readonly RefRuntimeInit<ZiHealth> healthRef = new RefRuntimeInit<ZiHealth>();
    private readonly RefRuntimeInit<ZiAura> auraRef = new RefRuntimeInit<ZiAura>();

    private void Awake()
    {
        ziRef.GetOrInit(InitZi);
        healthRef.GetOrInit(InitZiHealth);
        auraRef.GetOrInit(InitZiAura);
    }

    private Zi InitZi()
    {
        return GetComponent<Zi>();
    }

    private ZiHealth InitZiHealth()
    {
        return GetComponentInChildren<ZiHealth>();
    }

    private ZiAura InitZiAura()
    {
        return GetComponent<ZiAura>().Constructor(Health.GetHealthPercent());
    }
}
