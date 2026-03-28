using Stellar.Kernel;

namespace Stellar.Core.Quantization;

public class TimedMetaQuant : MetaQuant
{
    public DateTime CreatedAt { get; }
    public DateTime StartAt { get; private set; }
    public TimeSpan? Lifetime { get; private set; }

    public TimedMetaQuant(DateTime? startAt = null, float? lifetimeSeconds = null, IIdentifier? identifier = null)
        : base(identifier)
    {
        CreatedAt = DateTime.UtcNow;
        if (startAt.HasValue && lifetimeSeconds.HasValue)
        {
            SetLifetime(startAt.Value, lifetimeSeconds.Value);
        }
    }

    public TimedMetaQuant(float? lifetimeSeconds = null, IIdentifier? identifier = null)
        : this(DateTime.UtcNow, lifetimeSeconds, identifier)
    {
    }

    public TimeSpan Age => DateTime.UtcNow - CreatedAt;
    public DateTime? ExpiresAt => StartAt + Lifetime;
    public TimeSpan? ExpiresIn => ExpiresAt - DateTime.Now;
    public bool IsExpired => ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value;

    public void SetLifetime(DateTime startAt, TimeSpan lifetime)
    {
        StartAt = startAt;
        Lifetime = lifetime;
    }

    public void SetLifetime(TimeSpan lifetime) => SetLifetime(DateTime.UtcNow, lifetime);

    public void SetLifetime(DateTime startAt, float lifetime) => SetLifetime(startAt, TimeSpan.FromSeconds(lifetime));

    public void SetLifetime(float lifetime) => SetLifetime(DateTime.UtcNow, lifetime);

    public void RessetLifetime() => StartAt = DateTime.UtcNow;

    public Task SetLifetimeAsync(DateTime startAt, TimeSpan lifetime)
    {
        SetLifetime(startAt, lifetime);
        return Task.CompletedTask;
    }

    public Task SetLifetimeAsync(TimeSpan lifetime)
    {
        SetLifetime(lifetime);
        return Task.CompletedTask;
    }

    public Task SetLifetimeAsync(DateTime startAt, float lifetime)
    {
        SetLifetime(startAt, lifetime);
        return Task.CompletedTask;
    }

    public Task SetLifetimeAsync(float lifetime)
    {
        SetLifetime(lifetime);
        return Task.CompletedTask;
    }
    
    public Task RessetLifetimeAsync()
    {
        RessetLifetime();
        return Task.CompletedTask;
    }
}