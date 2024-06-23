using Zenject;

public class SceneLoadService
{
    private readonly ZenjectSceneLoader _sceneLoader;
    
    public SceneLoadService(ZenjectSceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    
    public void Load(string name)
    {
        _sceneLoader.LoadScene(name);
    }
}
