using Stellar.Kernel;

namespace Stellar.Core.Quantization;

public class TimedMetaQuant : MetaQuant
{
    public DateTime CreatedAt { get; }
    public TimeSpan Age => DateTime.UtcNow - CreatedAt;

    public DateTime StartAt { get; private set; }
    public DateTime? ExpiresAt { get; private set; }
    public TimeSpan Lifetime => ExpiresAt?.Subtract(StartAt) ?? TimeSpan.MaxValue;
    public TimeSpan Duration => Age - Lifetime;
    public TimeSpan? ExpiresIn => ExpiresAt - DateTime.Now;
    public bool IsExpired => ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value;

    public void SetLifetime(TimeSpan lifetime)
    {
        StartAt = DateTime.UtcNow;
        ExpiresAt = DateTime.UtcNow.Add(lifetime);
    }

    public void SetLifetime(float seconds) => SetLifetime(TimeSpan.FromSeconds(seconds));

    public TimedMetaQuant(IIdentifier? identifier = null, float? lifetimeSeconds = null)
        : base(identifier)
    {
        CreatedAt = DateTime.UtcNow;
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