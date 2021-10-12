using Desdiene.MonoBehaviourExtension;
using UnityEngine;

/// <summary>
/// Описывает систему частиц "Glowing particle" как модель.
/// </summary>
public class GlowingParticle : MonoBehaviourExt
{
    [SerializeField, NotNull] private ParticleSystem _particleSystem;

    public void SetConstantVelocity(Vector2 newVelocity)
    {
        ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime = _particleSystem.velocityOverLifetime;
        velocityOverLifetime.x = new ParticleSystem.MinMaxCurve(newVelocity.x);
        velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(newVelocity.y);
    }
}
