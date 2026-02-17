using Stellar.Kernel;

namespace Stellar.Core.Quantization;

public class TimedMetaQuant : MetaQuant
{
    public DateTime CreatedAt { get; }
    public DateTime? ExpiresAt { get; private set; }
    public TimeSpan Lifetime => ExpiresAt?.Subtract(CreatedAt) ?? TimeSpan.MaxValue;
    public TimeSpan Age => DateTime.UtcNow - CreatedAt;
    public bool IsExpired => ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value;

    public void SetLifetime(TimeSpan lifetime) => ExpiresAt = DateTime.UtcNow.Add(lifetime);
    public void SetLifetime(float seconds) => ExpiresAt = DateTime.UtcNow.AddSeconds(seconds);

    public TimedMetaQuant(IIdentifier? identifier = null, float? lifetimeSeconds = null)
        : base(identifier)
    {
        if (lifetimeSeconds.HasValue)
        {
            SetLifetime(lifetimeSeconds.Value);
        }
    }

    public void RessetLifetime()
    {
        if (Lifetime != TimeSpan.MaxValue)
        {
            SetLifetime(Lifetime);
        }
    }
}