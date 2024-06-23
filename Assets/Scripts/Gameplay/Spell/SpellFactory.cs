using System;

public class SpellFactory
{
    private readonly IUnitView _sourceView;
    private readonly UnitRole _roleForTargets;
    private readonly UnitService _unitService;
    private readonly ProjectileService _projectileService;

    public SpellFactory(IUnitView sourceView, UnitRole roleForTargets, UnitService unitService,
        ProjectileService projectileService)
    {
        _sourceView = sourceView;
        _roleForTargets = roleForTargets;
        _unitService = unitService;
        _projectileService = projectileService;
    }

    public ISpell Create(ISpellConfig config) =>
        config switch
        {
            ChainLightningSpellConfig c => new ChainLightningSpell(c, _sourceView, _roleForTargets, _unitService),
            FireBallSpellConfig c => new FireBallSpell(c, _sourceView, _roleForTargets, _unitService,
                _projectileService),
            FrostCircleSpellConfig c => new FrostCircleSpell(c, _sourceView, _roleForTargets, _unitService),

            _ => throw new ArgumentOutOfRangeException(nameof(config), config, "wrong spell config")
        };
}