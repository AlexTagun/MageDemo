using UnityEngine;

public class CameraView : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public Camera Camera => _camera;
}