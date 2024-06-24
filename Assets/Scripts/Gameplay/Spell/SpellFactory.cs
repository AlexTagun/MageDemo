using System;
using Zenject;

public class SpellFactory
{
    private readonly UnitService _unitService;
    private readonly ProjectileService _projectileService;

    [Inject]
    public SpellFactory(UnitService unitService, ProjectileService projectileService)
    {
        _unitService = unitService;
        _projectileService = projectileService;
    }

    public ISpell Create(ISpellConfig config, IUnitView sourceView, UnitRole targetsRole) =>
        config switch
        {
            ChainLightningSpellConfig c => new ChainLightningSpell(c, sourceView, targetsRole, _unitService),
            FireBallSpellConfig c => new FireBallSpell(c, sourceView, targetsRole, _unitService,
                _projectileService),
            FrostCircleSpellConfig c => new FrostCircleSpell(c, sourceView, targetsRole, _unitService),

            _ => throw new ArgumentOutOfRangeException(nameof(config), config, "wrong spell config")
        };
}