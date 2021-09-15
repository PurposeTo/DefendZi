using Desdiene.MonoBehaviourExtension;
using UnityEngine;

/// <summary>
/// Описывает систему частиц "Glowing particle" как модель.
/// </summary>
public class GlowingParticle : MonoBehaviourExt
{
    [SerializeField, NotNull] private ParticleSystem _particleSystem;

    public void SetConstantSpeedOx(float speed)
    {
        ParticleSystem.VelocityOverLifetimeModule velocity = _particleSystem.velocityOverLifetime;
        velocity.x = new ParticleSystem.MinMaxCurve(speed);
    }
}
