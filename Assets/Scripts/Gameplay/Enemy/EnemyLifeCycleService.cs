using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EnemyLifeCycleService : IStart, IUpdate
{
    private class Context
    {
        public float Damage;

        public Context(float damage)
        {
            Damage = damage;
        }
    }
    
    [Inject] private CameraService _cameraService;
    [Inject] private GameplaySettings _settings;
    [Inject] private EnemyFactory _enemyFactory;
    [Inject] private EnemyMovementService _movement;

    private readonly Dictionary<EnemyView, Context> _contexts = new();
    private readonly List<EnemyView> _enemiesToDestroy = new();

    private CameraView _cameraView;
    private TimeSpan _timer;


    void IStart.Start()
    {
        _cameraView = _cameraService.CameraView;
        _timer = _settings.EnemySpawnCooldown;
    }

    void IUpdate.Update()
    {
        SpawnUpdate();
        DestroyUpdate();
    }

    public float GetDamage(EnemyView enemyView) => _contexts[enemyView].Damage;

    private void SpawnUpdate()
    {
        _timer -= TimeSpan.FromSeconds(Time.deltaTime);

        if (_timer.TotalSeconds > 0 || _contexts.Count >= _settings.EnemySpawnMaxCount)
        {
            return;
        }

        _timer = _settings.EnemySpawnCooldown;
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        var center = _cameraView.transform.position;
        var offset = Random.insideUnitCircle.normalized * _settings.EnemySpawnRadius;
        var position = new Vector3(offset.x, 0, offset.y) + center;

        var config = _settings.GetRandomEnemyConfig();

        var view = _enemyFactory.Create(config, position, _enemiesToDestroy);
        _movement.Add(view, config.MovementSpeed);
        _contexts.Add(view, new Context(config.Damage));
    }

    private void DestroyUpdate()
    {
        foreach (var view in _enemiesToDestroy)
        {
            _movement.Remove(view);
            _contexts.Remove(view);
            Object.Destroy(view.gameObject);
        }

        _enemiesToDestroy.Clear();
    }
}