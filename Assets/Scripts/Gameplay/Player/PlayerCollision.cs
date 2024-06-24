using System;
using UnityEngine;
using Zenject;

public class PlayerCollision : IStart, IUpdate
{
    private readonly IPlayerViewProvider _playerViewProvider;
    private readonly UnitService _unitService;
    private readonly EnemyLifeCycleService _enemyLifeCycleService;
    private readonly TimeSpan _immunityTime;

    private TimeSpan _timer;

    [Inject]
    public PlayerCollision(IPlayerViewProvider playerViewProvider, UnitService unitService,
        EnemyLifeCycleService enemyLifeCycleService, GameplaySettings gameplaySettings)
    {
        _playerViewProvider = playerViewProvider;
        _unitService = unitService;
        _enemyLifeCycleService = enemyLifeCycleService;
        _immunityTime = gameplaySettings.PlayerSettings.ImmunityTime;
    }

    void IStart.Start()
    {
        _playerViewProvider.GetView().CollisionStayed += OnCollisionStayed;
    }

    void IUpdate.Update()
    {
        _timer -= TimeSpan.FromSeconds(Time.deltaTime);
    }

    private void OnCollisionStayed(Collision collision)
    {
        if (_timer.TotalSeconds > 0)
        {
            return;
        }

        var enemyView = collision.collider.GetComponent<EnemyView>();

        if (enemyView == null)
        {
            return;
        }

        var damage = _enemyLifeCycleService.GetDamage(enemyView);
        var context = new DamageContext(enemyView, _playerViewProvider.GetView(), damage);
        _unitService.TakeDamage(context);
        _timer = _immunityTime;
    }
}