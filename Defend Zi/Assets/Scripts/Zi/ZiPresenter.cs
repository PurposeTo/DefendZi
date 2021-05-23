using UnityEngine;
using Desdiene.Types.AtomicReference.RefRuntimeInit;

[RequireComponent(typeof(Zi))]
public class ZiPresenter : MonoBehaviour
{
    public Zi Zi => ziRef.Get(InitZi);
    public ZiHealth Health => healthRef.Get(InitZiHealth);
    public ZiAura Aura => auraRef.Get(InitZiAura);

    private readonly RefRuntimeInit<Zi> ziRef = new RefRuntimeInit<Zi>();
    private readonly RefRuntimeInit<ZiHealth> healthRef = new RefRuntimeInit<ZiHealth>();
    private readonly RefRuntimeInit<ZiAura> auraRef = new RefRuntimeInit<ZiAura>();

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
        return GetComponent<ZiAura>().Constructor(Health.GetHealthPercent());
    }
}
