using UnityEngine;
using Desdiene.AtomicReference;

[RequireComponent(typeof(Zi))]
public class ZiPresenter : MonoBehaviour
{
    public Zi Zi => ziRef.Get(InitZi);
    public ZiHealth Health => healthRef.Get(InitZiHealth);
    public ZiAura Aura => auraRef.Get(InitZiAura);

    private readonly AtomicRefRuntimeInit<Zi> ziRef = new AtomicRefRuntimeInit<Zi>();
    private readonly AtomicRefRuntimeInit<ZiHealth> healthRef = new AtomicRefRuntimeInit<ZiHealth>();
    private readonly AtomicRefRuntimeInit<ZiAura> auraRef = new AtomicRefRuntimeInit<ZiAura>();

    private void Awake()
    {
        ziRef.Initialize(InitZi);
        healthRef.Initialize(InitZiHealth);
        auraRef.Initialize(InitZiAura);
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
        return GetComponentInChildren<ZiAura>().Constructor(Health.GetHealthPercent());
    }
}
