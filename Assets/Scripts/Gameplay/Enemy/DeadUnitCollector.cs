using System.Collections.Generic;

public class DeadUnitCollector<T> : IDeathHandler where T : IUnitView
{
    private readonly List<T> _collection;
    private readonly T _view;

    public DeadUnitCollector(List<T> collection, T view)
    {
        _collection = collection;
        _view = view;
    }

    public void OnDeath(IUnitView source)
    {
        _collection.Add(_view);
    }
}