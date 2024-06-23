using System.Collections.Generic;

public class FrostCircleSpell : ISpell
{
    private readonly FrostCircleSpellConfig _config;
    private readonly IUnitView _sourceView;
    private readonly UnitRole _roleForTargets;
    private readonly UnitService _unitService;
    private readonly List<IUnitView> _buffer = new();

    public FrostCircleSpell(FrostCircleSpellConfig config, IUnitView sourceView, UnitRole roleForTargets,
        UnitService unitService)
    {
        _config = config;
        _sourceView = sourceView;
        _roleForTargets = roleForTargets;
        _unitService = unitService;
    }

    public void Cast()
    {
        _unitService.GetAllUnitsInCircleByRole(_sourceView.GetPosition(), _config.Radius, _roleForTargets, _buffer);

        foreach (var unitView in _buffer)
        {
            var context = new DamageContext(_sourceView, unitView, _config.Damage);
            _unitService.TakeDamage(context);
        }
    }
}