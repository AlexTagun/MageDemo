using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "GameplaySettings", menuName = "Gameplay/Settings")]
public class GameplaySettings : ScriptableObjectInstaller<GameplayAssetsInstaller>
{
    [SerializeField] private PlayerSettings _playerSettings;
    [SerializeField] private float _enemySpawnRadius;
    [SerializeField] private float _enemySpawnCooldownInSeconds;
    [SerializeField] private float _enemySpawnMaxCount;
    [SerializeField] private List<EnemyConfigWithWeight> _enemyConfigs;

    public PlayerSettings PlayerSettings => _playerSettings;
    public float EnemySpawnRadius => _enemySpawnRadius;
    public TimeSpan EnemySpawnCooldown => TimeSpan.FromSeconds(_enemySpawnCooldownInSeconds);
    public float EnemySpawnMaxCount => _enemySpawnMaxCount;

    public EnemyConfig GetRandomEnemyConfig()
    {
        var totalWeight = _enemyConfigs.Sum(weighted => weighted.Weight);

        var randomValue = Random.Range(0f, totalWeight);
        foreach (var element in _enemyConfigs)
        {
            if (randomValue < element.Weight)
                return element.Config;

            randomValue -= element.Weight;
        }

        return _enemyConfigs[0].Config;
    }
}