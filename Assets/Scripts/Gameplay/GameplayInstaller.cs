using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private UiBindings _uiBindings;

    public override void InstallBindings()
    {
        Container.BindInstance(_uiBindings);

        Container.Bind<IMovementInput>().To<KeyboardMovementInput>().AsSingle();

        Container.Bind<HealthFactory>().AsSingle();
        Container.Bind<HealthBarFactory>().AsSingle();

        Container.Bind<UnitService>().AsSingle();

        Container.BindInterfacesAndSelfTo<ProjectileService>().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerViewProvider>().AsSingle();
        Container.BindInterfacesAndSelfTo<CameraViewProvider>().AsSingle();

        Container.BindInterfacesAndSelfTo<CameraService>().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerService>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerCollision>().AsSingle();
        Container.BindInterfacesAndSelfTo<SpellFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerSpellsController>().AsSingle();

        Container.BindInterfacesAndSelfTo<EnemyLifeCycleService>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyMovementService>().AsSingle();

        Container.BindInterfacesAndSelfTo<LoseWindowPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthBarService>().AsSingle();
    }
}