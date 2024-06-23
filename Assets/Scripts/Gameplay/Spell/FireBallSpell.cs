public class FireBallSpell : ISpell
{
    private readonly FireBallSpellConfig _config;
    private readonly IUnitView _sourceView;
    private readonly UnitRole _roleForTargets;
    private readonly UnitService _unitService;
    private readonly ProjectileService _projectileService;

    public FireBallSpell(FireBallSpellConfig config, IUnitView sourceView, UnitRole roleForTargets,
        UnitService unitService, ProjectileService projectileService)
    {
        _config = config;
        _sourceView = sourceView;
        _roleForTargets = roleForTargets;
        _unitService = unitService;
        _projectileService = projectileService;
    }

    public void Cast()
    {
        var origin = _sourceView.GetPosition();
        if (!_unitService.TryGetNearestUnit(origin, _roleForTargets, out var nearestUnit))
        {
            return;
        }

        var request = new CreateProjectileRequest
        {
            Origin = origin,
            Direction = (nearestUnit.GetPosition() - origin).normalized,
            Speed = _config.Speed,
            ViewPrefab = _config.ViewPrefab,
            Damage = _config.Damage,
            RoleForTargets = _roleForTargets,
            SourceView = _sourceView
        };

        _projectileService.Create(request);
    }
}