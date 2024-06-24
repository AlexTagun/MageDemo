using System.Collections.Generic;
using Zenject;
using Object = UnityEngine.Object;

public class PlayerService : IStart, IPlayerViewProvider
{
    private readonly PlayerView _playerViewPrefab;
    private readonly UnitService _unitService;
    private readonly GameplaySettings _settings;
    private readonly LoseWindowPresenter _loseWindowPresenter;

    private PlayerView _playerView;

    [Inject]
    public PlayerService(PlayerView playerViewPrefab, UnitService unitService, GameplaySettings settings,
        LoseWindowPresenter loseWindowPresenter)
    {
        _playerViewPrefab = playerViewPrefab;
        _unitService = unitService;
        _settings = settings;
        _loseWindowPresenter = loseWindowPresenter;
    }

    void IStart.Start()
    {
        _playerView = Object.Instantiate(_playerViewPrefab);
        _playerView.Init();

        CreatePlayerUnit();
    }

    PlayerView IPlayerViewProvider.GetView() => _playerView;

    private void CreatePlayerUnit()
    {
        var id = _playerView.gameObject.name;
        var healthChangedHandlers = new List<IHealthChangedHandler>
        {
            new DebugHealthChangedHandler(id),
        };

        var deathHandlers = new List<IDeathHandler>
        {
            new DebugDeathHandler(id), _loseWindowPresenter
        };

        _unitService.Create(_playerView,
            _settings.PlayerSettings.HealthConfig,
            UnitRole.Player,
            healthChangedHandlers,
            deathHandlers);
    }
}