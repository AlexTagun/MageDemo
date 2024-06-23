using System;
using UnityEngine;

[Serializable]
public class HealthWithDefenceConfig : IHealthConfig
{
    public float MaxHealth;
    [Range(0, 1)] public float Defence;
}