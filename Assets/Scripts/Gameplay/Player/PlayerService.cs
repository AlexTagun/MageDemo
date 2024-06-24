using System.Collections.Generic;
using Zenject;

public class PlayerService : IStart
{
    private readonly PlayerViewProvider _playerViewProvider;
    private readonly UnitService _unitService;
    private readonly GameplaySettings _settings;
    private readonly LoseWindowPresenter _loseWindowPresenter;
    private readonly HealthBarView _healthBarPrefab;
    private readonly HealthBarService _healthBarService;

    [Inject]
    public PlayerService(PlayerViewProvider playerViewProvider, UnitService unitService, GameplaySettings settings,
        LoseWindowPresenter loseWindowPresenter, HealthBarAssets healthBarAssets, HealthBarService healthBarService)
    {
        _playerViewProvider = playerViewProvider;
        _unitService = unitService;
        _settings = settings;
        _loseWindowPresenter = loseWindowPresenter;
        _healthBarPrefab = healthBarAssets.Player;
        _healthBarService = healthBarService;
    }

    void IStart.Start()
    {
        CreatePlayerUnit();
    }

    private void CreatePlayerUnit()
    {
        var view = _playerViewProvider.GetView();
        var id = view.gameObject.name;
        var healthChangedHandlers = new List<IHealthChangedHandler>
        {
            new DebugHealthChangedHandler(id),
        };

        var deathHandlers = new List<IDeathHandler>
        {
            new DebugDeathHandler(id), _loseWindowPresenter
        };

        _healthBarService.Create(view, _healthBarPrefab, healthChangedHandlers, deathHandlers);

        _unitService.Create(view,
            _settings.PlayerSettings.HealthConfig,
            UnitRole.Player,
            healthChangedHandlers,
            deathHandlers);
    }
}