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
        public readonly float Damage;

        public Context(float damage)
        {
            Damage = damage;
        }
    }

    [Inject] private CameraService _cameraService;
    [Inject] private GameplaySettings _settings;
    [Inject] private EnemyFactory _enemyFactory;
    [Inject] private EnemyMovementService _movement;
    [Inject] private UnitService _unitService;

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

        if (TryGenerateRandomPositionOutCameraOnGround(out var position))
        {
            SpawnEnemy(position);
        }
    }

    private bool TryGenerateRandomPositionOutCameraOnGround(out Vector3 position)
    {
        var x = Random.value;
        var y = Random.value;

        if (Random.value > 0.5f)
        {
            x = x > 0.5f ? 1.1f : -0.1f;
        }
        else
        {
            y = y > 0.5f ? 1.1f : -0.1f;
        }

        var cameraPos = _cameraView.Camera.transform.position;
        position = _cameraView.Camera.ViewportToWorldPoint(new Vector3(x, y, cameraPos.y));
        return position.x is < 30 and > -30 && position.z is < 30 and > -30;
    }

    private void SpawnEnemy(Vector3 position)
    {
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
            _unitService.Remove(view);
            Object.Destroy(view.gameObject);
        }

        _enemiesToDestroy.Clear();
    }
}