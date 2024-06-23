using System.Collections.Generic;
using Zenject;
using Object = UnityEngine.Object;

public class PlayerService : IStart, IUpdate
{
    [Inject] private IMovementInput _movementInput;
    [Inject] private PlayerView _playerViewPrefab;
    [Inject] private UnitService _unitService;
    [Inject] private ProjectileService _projectileService;
    [Inject] private GameplaySettings _settings;

    private PlayerMovement _movement;
    private PlayerSpellsController _spellsController;

    public PlayerView PlayerView { get; private set; }

    void IStart.Start()
    {
        var playerSettings = _settings.PlayerSettings;

        PlayerView = Object.Instantiate(_playerViewPrefab);
        PlayerView.Init();

        _movement = new PlayerMovement(_movementInput, PlayerView, playerSettings);

        var spellFactory = new SpellFactory(PlayerView, UnitRole.Enemy, _unitService, _projectileService);
        _spellsController = new PlayerSpellsController(spellFactory, playerSettings);
        _spellsController.Init();

        CreatePlayerUnit();
    }

    void IUpdate.Update()
    {
        _movement.Update();
        _spellsController.Update();
    }

    private void CreatePlayerUnit()
    {
        var id = PlayerView.gameObject.name;
        var healthChangedCollection = new List<IHealthChanged>
        {
            new DebugHealthChanged(id),
        };

        var deathCollection = new List<IDeath>
        {
            new DebugDeath(id),
        };

        _unitService.Create(PlayerView,
            _settings.PlayerSettings.HealthConfig,
            UnitRole.Player,
            healthChangedCollection,
            deathCollection);
    }
}