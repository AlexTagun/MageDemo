using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory
{
    [Inject] private UnitService _unitService;

    private int _count;

    public EnemyView Create(EnemyConfig config, Vector3 position, List<EnemyView> enemiesToDestroy)
    {
        var view = Object.Instantiate(config.ViewPrefab, position, Quaternion.identity);
        view.Init();

        var id = $"enemy_{_count}";
        view.gameObject.name = id;

        var healthChangedCollection = new List<IHealthChanged>
        {
            new DebugHealthChanged(id),
        };

        var deathCollection = new List<IDeath>
        {
            new DebugDeath(id),
            new AddEnemyViewToCollectionOnDeath(enemiesToDestroy, view),
        };

        _unitService.Create(view,
            config.HealthConfig,
            UnitRole.Enemy,
            healthChangedCollection,
            deathCollection);

        _count++;

        return view;
    }
}