using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Core.Quantization;
using Stellar.Kernel.Data.Collections;

namespace Stellar.Core.Data.Collections;


public class Roster<T> 
    : WritableTable<T>
    where T : IQuant
{
    public ConcurrentIdentifierMap<Roster<T>> Branches { get; }
    
    #region Constructors

    // ReSharper disable once ConvertToPrimaryConstructor
    public Roster(MetaQuant metaData, Dictionary<IIdentifier, T> data, Dictionary<IIdentifier, Roster<T>> branches)
        : base(metaData, data)
    {
        Branches = new ConcurrentIdentifierMap<Roster<T>>(branches);
    }

    public Roster(MetaQuant metaData, Dictionary<IIdentifier, T> data)
        : this(metaData, data, [])
    {
    }
    
    public Roster(MetaQuant metaData)
        : this(metaData, [])
    {
    }
    
    // TODO: implement DataContainer<T> parametrized constructors
        
    // public Roster(MetaQuant meta, DataContainer<T> container)
    //     : base(meta, container.Data)
    // {
    // }
    //
    // public Roster(DataContainer<T> container)
    //     : base(container.MetaQuant, container.Data)
    // {
    // }
    
    #endregion

    #region Branch operations

    public Roster<T> GetBranch(IIdentifier identifier)
    {
        return Branches.GetValueOrDefault(identifier) ?? throw new KeyNotFoundException();
    }

    public bool SaveBranch(Roster<T> branch)
    {
        return Branches.TryAdd(branch.UID, branch);
    }
    
    public Roster<T>? PopBranch(IIdentifier identifier)
    {
        Branches.TryRemove(identifier, out var branch);
        return branch;
    }
    
    public Roster<T> NewBranch(MetaQuant meta, Dictionary<IIdentifier, T> data)
    {
        var branch = new Roster<T>(meta, data);
        SaveBranch(branch);
        return branch;
    }
        
    public Roster<T> NewBranch(MetaQuant metaData) => NewBranch(metaData, new Dictionary<IIdentifier, T>());
    
    public bool ContainsBranch(IIdentifier identifier) => Branches.ContainsKey(identifier);

    #endregion
}
