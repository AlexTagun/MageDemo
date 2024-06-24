using UnityEngine;
using Zenject;

public class CameraViewProvider : IStart
{
    private readonly CameraView _cameraViewPrefab;
    
    private CameraView _cameraView;

    [Inject]
    public CameraViewProvider(CameraView cameraViewPrefab)
    {
        _cameraViewPrefab = cameraViewPrefab;
    }
    
    void IStart.Start()
    {
        _cameraView = Object.Instantiate(_cameraViewPrefab, Vector3.zero, Quaternion.identity);
    }
    
    public CameraView GetView() => _cameraView;

}