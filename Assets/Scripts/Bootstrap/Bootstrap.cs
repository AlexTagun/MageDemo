using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    [Inject] private SceneLoadService _sceneLoadService;
    
    private void Awake()
    {
        _sceneLoadService.Load("Gameplay");
    }
}