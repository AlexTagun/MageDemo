using System.Collections.Generic;
using UnityEngine;

public class UnitService
{
    private class Unit
    {
        public IHealth Health;
        public UnitRole UnitRole;
        public List<IHealthChanged> HealthChangedCollection;
        public List<IDeath> DeathCollection;
        public bool IsDead;
    }

    private readonly HealthFactory _healthFactory = new();
    private readonly Dictionary<IUnitView, Unit> _units = new();


    public void Create(IUnitView unitView,
        IHealthConfig healthConfig,
        UnitRole unitRole,
        List<IHealthChanged> healthChangedCollection,
        List<IDeath> deathCollection)
    {
        var unit = new Unit
        {
            Health = _healthFactory.Create(healthConfig),
            UnitRole = unitRole,
            HealthChangedCollection = healthChangedCollection,
            DeathCollection = deathCollection,
        };

        _units.Add(unitView, unit);
    }

    public void Remove(IUnitView view) => _units.Remove(view);

    public bool IsExist(IUnitView view) => _units.ContainsKey(view);

    public UnitRole GetRole(IUnitView view) => _units[view].UnitRole;

    public void TakeDamage(DamageContext context)
    {
        var receiver = context.Receiver;
        var unit = _units[receiver];
        var health = unit.Health;

        health.ReceiveHit(context.Damage);

        if (unit.IsDead)
        {
            return;
        }

        HealthChangedContext healthChangedContext = new(context, CalculateCurrentPercentage(health));

        foreach (var healthChanged in unit.HealthChangedCollection)
            healthChanged.OnHealthChanged(healthChangedContext);

        if (unit.Health.GetCurrent() > 0)
        {
            return;
        }

        foreach (var death in unit.DeathCollection)
            death.OnDeath(context.Source);

        unit.IsDead = true;
    }

    public void Heal(HealContext context)
    {
        var receiver = context.Receiver;
        var unit = _units[receiver];
        var health = unit.Health;

        health.ReceiveHeal(context.Value);

        HealContext unitHealContext = new(context.Source, context.Receiver, context.Value);

        HealthChangedContext healthChangedContext = new(unitHealContext, CalculateCurrentPercentage(health));
        foreach (var healthChanged in unit.HealthChangedCollection)
            healthChanged.OnHealthChanged(healthChangedContext);
    }

    public void GetAllUnitsInCircleByRole(Vector3 position,
        float radius,
        UnitRole role,
        ICollection<IUnitView> buffer)
    {
        buffer.Clear();
        foreach (var (view, unit) in _units)
        {
            if (unit.UnitRole != role)
                continue;

            if (radius < Vector3.Distance(view.GetPosition(), position))
                continue;

            buffer.Add(view);
        }
    }

    public bool TryGetNearestUnit(Vector3 position, UnitRole role, out IUnitView nearestUnit)
    {
        nearestUnit = null;
        var minDistance = float.MaxValue;
        var nearestUnitExist = false;

        foreach (var (view, unit) in _units)
        {
            if (unit.UnitRole != role)
                continue;

            var distance = Vector3.Distance(view.GetPosition(), position);

            if (minDistance <= distance)
                continue;

            minDistance = distance;
            nearestUnit = view;
            nearestUnitExist = true;
        }

        return nearestUnitExist;
    }

    private static float CalculateCurrentPercentage(IHealth health) => health.GetCurrent() / health.GetMax();
}