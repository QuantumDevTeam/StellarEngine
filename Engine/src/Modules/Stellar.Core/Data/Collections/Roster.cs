using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Core.Quantization;

namespace Stellar.Core.Data.Collections;


public class Roster<T> 
    : WritableTable<T>
    where T : IQuant
{
    public ConcurrentDictionary<IIdentifier, Roster<T>> Branches { get; } = [];
    
    #region Constructors
    
    public Roster(MetaData metaData)
        : base(metaData)
    {
    }

    public Roster(MetaData metaData, Dictionary<Identifier, T> data)
        : base(metaData, data)
    {
    }
        
    public Roster(MetaQuant meta, DataContainer<T> container)
        : base(meta, container.Data)
    {
    }
    
    public Roster(DataContainer<T> container)
        : base(container.MetaQuant, container.Data)
    {
    }
    
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
        if (Branches.TryRemove(identifier, out var branch)) return branch;
    }
    
    public Roster<T> NewBranch(MetaQuant meta, Dictionary<IIdentifier, T> data)
    {
        var branch = new Roster<T>(meta, data);
        SaveBranch(branch);
        return branch;
    }
        
    public Roster<T> NewBranch(MetaData metaData) => NewBranch(metaData, new Dictionary<IIdentifier, T>());
    
    public Roster<T> ContainsBranch(IIdentifier identifier) => Branches.ContainsKey(identifier);

    #endregion
}
