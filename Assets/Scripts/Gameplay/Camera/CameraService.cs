using UnityEngine;
using Zenject;

public class CameraService : IStart, IUpdate, ICameraViewProvider
{
    private readonly IPlayerViewProvider _playerViewProvider;
    private readonly CameraView _cameraViewPrefab;

    private PlayerView _playerView;
    private CameraView _cameraView;

    [Inject]
    public CameraService(IPlayerViewProvider playerViewProvider, CameraView cameraViewPrefab)
    {
        _playerViewProvider = playerViewProvider;
        _cameraViewPrefab = cameraViewPrefab;
    }

    void IStart.Start()
    {
        _playerView = _playerViewProvider.GetView();
        _cameraView = Object.Instantiate(_cameraViewPrefab, _playerView.GetPosition(), Quaternion.identity);
    }

    void IUpdate.Update()
    {
        _cameraView.transform.position = _playerView.transform.position;
    }

    CameraView ICameraViewProvider.GetView() => _cameraView;
}