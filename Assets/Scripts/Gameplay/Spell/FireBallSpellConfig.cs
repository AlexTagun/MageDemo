using System;

[Serializable]
public class FireBallSpellConfig : ISpellConfig
{
    public ProjectileView ViewPrefab;
    public float Damage;
    public float Speed;
    public float OriginRadius;
}