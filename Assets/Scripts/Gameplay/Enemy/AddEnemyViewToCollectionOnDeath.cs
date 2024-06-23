using System.Collections.Generic;

public class AddEnemyViewToCollectionOnDeath : IDeath
{
    private readonly List<EnemyView> _collection;
    private readonly EnemyView _view;

    public AddEnemyViewToCollectionOnDeath(List<EnemyView> collection, EnemyView view)
    {
        _collection = collection;
        _view = view;
    }

    public void OnDeath(IUnitView source)
    {
        _collection.Add(_view);
    }
}