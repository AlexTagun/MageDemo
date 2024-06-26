﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChainLightningSpell : ISpell
{
    private readonly ChainLightningSpellConfig _config;
    private readonly List<IUnitView> _targetsBuffer = new();
    private readonly IUnitView _sourceView;
    private readonly UnitRole _targetsRole;
    private readonly UnitService _unitService;

    public ChainLightningSpell(ChainLightningSpellConfig config, IUnitView sourceView, UnitRole targetsRole,
        UnitService unitService)
    {
        _config = config;
        _sourceView = sourceView;
        _targetsRole = targetsRole;
        _unitService = unitService;
    }

    public void Cast()
    {
        var targets = new List<IUnitView>();
        if (!TryGetNearestTarget(_sourceView.GetPosition(), _config.Radius, out var nearestTarget, targets))
        {
            return;
        }

        targets.Add(nearestTarget);
        var nextTargetPosition = nearestTarget.GetPosition();
        for (var i = 0; i < _config.MaxEnemyCount - 1; i++)
        {
            if (!TryGetNearestTarget(nextTargetPosition, _config.Radius, out var nextTarget, targets))
            {
                break;
            }

            targets.Add(nextTarget);
            nextTargetPosition = nextTarget.GetPosition();
        }

        var damage = _config.Damage;

        foreach (var target in targets)
        {
            var context = new DamageContext(_sourceView, target, damage);
            _unitService.TakeDamage(context);
            damage -= damage * _config.DamageReductionForNextTargets;
        }

        var view = Object.Instantiate(_config.Prefab);
        var positions = new List<Vector3> { _sourceView.GetPosition() };
        positions.AddRange(targets.Select(target => target.GetPosition()));
        view.Show(positions.ToArray());
    }

    private bool TryGetNearestTarget(Vector3 origin, float radius, out IUnitView nearestTarget,
        ICollection<IUnitView> exceptTargets)
    {
        nearestTarget = null;
        _unitService.GetAllUnitsInCircleByRole(origin, radius, _targetsRole, _targetsBuffer);

        if (_targetsBuffer.Count <= 0)
        {
            return false;
        }

        var minDistance = float.MaxValue;
        var nearestTargetExist = false;

        foreach (var target in _targetsBuffer)
        {
            if (exceptTargets.Contains(target))
            {
                continue;
            }

            var distance = Vector3.Distance(origin, target.GetPosition());

            if (distance > minDistance)
            {
                continue;
            }

            minDistance = distance;
            nearestTarget = target;
            nearestTargetExist = true;
        }

        return nearestTargetExist;
    }
}