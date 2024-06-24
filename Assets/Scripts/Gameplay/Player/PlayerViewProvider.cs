using UnityEngine;
using Zenject;

public class PlayerViewProvider : IStart
{
    private readonly PlayerView _playerViewPrefab;
    private PlayerView _playerView;

    [Inject]
    public PlayerViewProvider(PlayerView playerViewPrefab)
    {
        _playerViewPrefab = playerViewPrefab;
    }
    
    void IStart.Start()
    {
        _playerView = Object.Instantiate(_playerViewPrefab);
        _playerView.Init();
    }
    
    public PlayerView GetView() => _playerView;
}