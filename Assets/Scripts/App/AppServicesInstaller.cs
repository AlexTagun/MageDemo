using Zenject;

public class AppServicesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SceneLoadService>().AsSingle();
    }
}