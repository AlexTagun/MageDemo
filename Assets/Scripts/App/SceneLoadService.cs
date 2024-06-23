using Zenject;

public class SceneLoadService
{
    private readonly ZenjectSceneLoader _sceneLoader;

    private const string Gameplay = "Gameplay";
    
    public SceneLoadService(ZenjectSceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    
    public void LoadGameplayScene()
    {
        _sceneLoader.LoadScene(Gameplay);
    }
}
