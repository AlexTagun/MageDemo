using Zenject;

public class CameraService : IUpdate
{
    private readonly CameraViewProvider _cameraViewProvider;
    private readonly PlayerViewProvider _playerViewProvider;

    [Inject]
    public CameraService(CameraViewProvider cameraViewProvider, PlayerViewProvider playerViewProvider)
    {
        _cameraViewProvider = cameraViewProvider;
        _playerViewProvider = playerViewProvider;
    }

    void IUpdate.Update()
    {
        var cameraView = _cameraViewProvider.GetView();
        var playerView = _playerViewProvider.GetView();
        cameraView.transform.position = playerView.GetPosition();
    }
}