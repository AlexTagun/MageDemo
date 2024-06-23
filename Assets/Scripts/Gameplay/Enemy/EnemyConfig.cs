using System;
using UnityEngine;

[Serializable]
public class EnemyConfig
{
    public EnemyView ViewPrefab;
    public float MovementSpeed;
    public float Damage;
    [SerializeReference] public IHealthConfig HealthConfig;
}