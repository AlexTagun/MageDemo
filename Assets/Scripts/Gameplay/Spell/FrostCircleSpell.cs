﻿using System.Collections.Generic;
using UnityEngine;

public class FrostCircleSpell : ISpell
{
    private readonly FrostCircleSpellConfig _config;
    private readonly IUnitView _sourceView;
    private readonly UnitRole _targetsRole;
    private readonly UnitService _unitService;
    private readonly List<IUnitView> _buffer = new();

    public FrostCircleSpell(FrostCircleSpellConfig config, IUnitView sourceView, UnitRole targetsRole,
        UnitService unitService)
    {
        _config = config;
        _sourceView = sourceView;
        _targetsRole = targetsRole;
        _unitService = unitService;
    }

    public void Cast()
    {
        _unitService.GetAllUnitsInCircleByRole(_sourceView.GetPosition(), _config.Radius, _targetsRole, _buffer);
        var view = Object.Instantiate(_config.Prefab, _sourceView.GetPosition(), Quaternion.identity);
        view.SetRadius(_config.Radius);
        Object.Destroy(view.gameObject, 0.3f);

        foreach (var unitView in _buffer)
        {
            var context = new DamageContext(_sourceView, unitView, _config.Damage);
            _unitService.TakeDamage(context);
        }
    }
}