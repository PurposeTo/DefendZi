using UnityEngine;
using Desdiene.AtomicReference;

[RequireComponent(typeof(Zi))]
public class ZiPresenter : MonoBehaviour
{
    public Zi Zi => ziRef.Get(InitZi);
    public ZiHealth ZiHealth => ziHealthRef.Get(InitZiHealth);
    public ZiAura ZiAura => ziAuraRef.Get(InitZiAura);

    private readonly AtomicRefRuntimeInit<Zi> ziRef = new AtomicRefRuntimeInit<Zi>();
    private readonly AtomicRefRuntimeInit<ZiHealth> ziHealthRef = new AtomicRefRuntimeInit<ZiHealth>();
    private readonly AtomicRefRuntimeInit<ZiAura> ziAuraRef = new AtomicRefRuntimeInit<ZiAura>();

    private void Awake()
    {
        ziRef.Initialize(InitZi);
        ziHealthRef.Initialize(InitZiHealth);
        ziAuraRef.Initialize(InitZiAura);
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
        return GetComponentInChildren<ZiAura>().Constructor(ZiHealth.GetHealthPercent());
    }
}
