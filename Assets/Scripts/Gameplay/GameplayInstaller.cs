using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private Canvas _canvas;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_canvas);
        Container.Bind<IMovementInput>().To<KeyboardMovementInput>().AsSingle();

        Container.Bind<HealthFactory>().AsSingle();
        Container.Bind<UnitService>().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerService>().AsSingle();
        Container.BindInterfacesAndSelfTo<CameraService>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerCollision>().AsSingle();
        Container.BindInterfacesAndSelfTo<SpellFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerSpellsController>().AsSingle();

        Container.BindInterfacesAndSelfTo<EnemyLifeCycleService>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyMovementService>().AsSingle();
        Container.BindInterfacesAndSelfTo<ProjectileService>().AsSingle();
        Container.BindInterfacesAndSelfTo<LoseWindowPresenter>().AsSingle();
    }
}