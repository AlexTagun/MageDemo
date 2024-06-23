using System;
using UnityEngine;

public class PlayerCollision
{
    private readonly PlayerView _view;
    private readonly UnitService _unitService;
    private readonly EnemyLifeCycleService _enemyLifeCycleService;
    private readonly TimeSpan _immunityTime;

    private TimeSpan _timer;

    public PlayerCollision(PlayerView view, UnitService unitService, EnemyLifeCycleService enemyLifeCycleService, TimeSpan immunityTime)
    {
        _view = view;
        _unitService = unitService;
        _enemyLifeCycleService = enemyLifeCycleService;
        _immunityTime = immunityTime;
    }

    public void Init()
    {
        _view.CollisionStayed += OnCollisionStayed;
    }

    public void Update()
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
        var context = new DamageContext(enemyView, _view, damage);
        _unitService.TakeDamage(context);
        _timer = _immunityTime;
    }
}