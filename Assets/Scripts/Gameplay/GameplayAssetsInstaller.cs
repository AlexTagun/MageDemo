using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameplayAssetsInstaller", menuName = "Installers/GameplayAssetsInstaller")]
public class GameplayAssetsInstaller : ScriptableObjectInstaller<GameplayAssetsInstaller>
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private CameraView _cameraView;
    [SerializeField] private GameplaySettings _gameplaySettings;
    [SerializeField] private LoseWindow _loseWindow;

    public override void InstallBindings()
    {
        Container.BindInstances(_playerView, _cameraView, _gameplaySettings, _loseWindow);
    }
}