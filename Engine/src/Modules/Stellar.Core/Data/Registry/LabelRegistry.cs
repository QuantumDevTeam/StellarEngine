// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract

using Stellar.Kernel;
using Stellar.Kernel.Data.Registry;

namespace Stellar.Core.Data.Registry;

public sealed class LabelRegistry
    : IRegistry<Label.Label>
{
    private readonly Dictionary<IIdentifier, Label.Label> _byId = new();
    private readonly Dictionary<string, Label.Label> _byName = new();
    private readonly ReaderWriterLockSlim _lock = new();

    private static readonly Lazy<LabelRegistry> _instance = new(() => new LabelRegistry());
    public static LabelRegistry Instance => _instance.Value;

    private LabelRegistry()
    {
    }

    public bool Exists(IIdentifier id)
    {
        if (id == null) return false;
        _lock.EnterReadLock();
        try
        {
            return _byId.ContainsKey(id);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public bool Register(Label.Label obj)
    {
        if (obj == null) return false;
        _lock.EnterWriteLock();
        try
        {
            if (_byName.ContainsKey(obj.Name))
                return false;
            _byId[obj.UID] = obj;
            _byName[obj.Name] = obj;
            return true;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public Label.Label? Get(IIdentifier id)
    {
        if (id == null) return null;
        _lock.EnterReadLock();
        try
        {
            return _byId.GetValueOrDefault(id);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public Label.Label? GetByName(string name)
    {
        if (string.IsNullOrEmpty(name)) return null;
        _lock.EnterReadLock();
        try
        {
            return _byName.GetValueOrDefault(name);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public Label.Label? Pop(IIdentifier id)
    {
        if (id == null) return null;
        _lock.EnterWriteLock();
        try
        {
            if (!_byId.Remove(id, out var label)) return null;
            _byName.Remove(label.Name);
            return label;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public int Size
    {
        get
        {
            _lock.EnterReadLock();
            try
            {
                return _byId.Count;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }

    public ICollection<IIdentifier> Keys
    {
        get
        {
            _lock.EnterReadLock();
            try
            {
                return _byId.Keys.ToArray();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }

    public ICollection<Label.Label> Values
    {
        get
        {
            _lock.EnterReadLock();
            try
            {
                return _byId.Values.ToArray();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }
}