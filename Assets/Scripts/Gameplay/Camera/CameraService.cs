using UnityEngine;
using Zenject;

public class CameraService : IStart, IUpdate
{
    [Inject] private PlayerService _playerService;
    [Inject] private CameraView _cameraViewPrefab;

    private PlayerView _playerView;

    public CameraView CameraView { get; private set; }

    void IStart.Start()
    {
        _playerView = _playerService.PlayerView;
        var transform = _playerView.transform;
        CameraView = Object.Instantiate(_cameraViewPrefab, transform.position, Quaternion.identity);
    }

    void IUpdate.Update()
    {
        CameraView.transform.position = _playerView.transform.position;
    }
}