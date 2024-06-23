using Zenject;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMovementInput>().To<KeyboardMovementInput>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<UnitService>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<PlayerService>().AsSingle();
        Container.BindInterfacesAndSelfTo<CameraService>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyLifeCycleService>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyMovementService>().AsSingle();
        Container.BindInterfacesAndSelfTo<ProjectileService>().AsSingle();
        Container.BindInterfacesAndSelfTo<LoseWindowPresenter>().AsSingle();
    }
}