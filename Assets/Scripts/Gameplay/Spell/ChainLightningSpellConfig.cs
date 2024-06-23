using System;
using UnityEngine;

[Serializable]
public class ChainLightningSpellConfig : ISpellConfig
{
    public ChainLightningView Prefab;
    public float Damage;
    public float Radius;
    public int MaxEnemyCount;
    [Range(0, 1)] public float DamageReductionForNextTargets;
}