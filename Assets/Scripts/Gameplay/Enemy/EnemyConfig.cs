using System;
using UnityEngine;

[Serializable]
public class EnemyConfig
{
    public EnemyView ViewPrefab;
    public float MovementSpeed;
    [SerializeReference] public IHealthConfig HealthConfig;
}