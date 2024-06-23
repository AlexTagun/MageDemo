using UnityEngine;

public class CreateProjectileRequest
{
    public Vector3 Origin;
    public Vector3 Direction;
    public float Speed;
    public ProjectileView ViewPrefab;
    public float Damage;
    public UnitRole RoleForTargets;
    public IUnitView SourceView;
}