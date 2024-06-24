using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory
{
    private readonly UnitService _unitService;
    private readonly HealthBarService _healthBarService;
    private readonly HealthBarView _healthBarPrefab;

    private int _count;

    [Inject]
    public EnemyFactory(UnitService unitService, HealthBarService healthBarService, HealthBarAssets healthBarAssets)
    {
        _unitService = unitService;
        _healthBarService = healthBarService;
        _healthBarPrefab = healthBarAssets.Enemy;
    }

    public EnemyView Create(EnemyConfig config, Vector3 position, List<EnemyView> enemiesToDestroy)
    {
        var view = Object.Instantiate(config.ViewPrefab, position, Quaternion.identity);
        view.Init();

        var id = $"enemy_{_count}";
        view.gameObject.name = id;

        var healthChangedHandlers = new List<IHealthChangedHandler>
        {
            new DebugHealthChangedHandler(id),
        };

        var deathHandlers = new List<IDeathHandler>
        {
            new DebugDeathHandler(id),
            new DeadUnitCollector<EnemyView>(enemiesToDestroy, view),
        };

        _healthBarService.Create(view, _healthBarPrefab, healthChangedHandlers, deathHandlers);

        _unitService.Create(view,
            config.HealthConfig,
            UnitRole.Enemy,
            healthChangedHandlers,
            deathHandlers);

        _count++;

        return view;
    }
}