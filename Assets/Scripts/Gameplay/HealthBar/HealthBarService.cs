using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HealthBarService : IUpdate
{
    private readonly RectTransform _canvasRectTransform;
    private readonly CameraViewProvider _cameraViewProvider;
    private readonly HealthBarFactory _healthBarFactory;

    private readonly List<HealthBarPresenter> _presenters = new();
    private readonly List<HealthBarPresenter> _presentersToDestroy = new();

    [Inject]
    public HealthBarService(UiBindings uiBindings, CameraViewProvider cameraViewProvider, HealthBarFactory healthBarFactory)
    {
        _canvasRectTransform = uiBindings.Canvas.transform as RectTransform;
        _cameraViewProvider = cameraViewProvider;
        _healthBarFactory = healthBarFactory;
    }
    
    void IUpdate.Update()
    {
        foreach (var presenter in _presentersToDestroy)
        {
            presenter.Destroy();
            _presenters.Remove(presenter);
        }

        _presentersToDestroy.Clear();

        foreach (var presenter in _presenters)
        {
            presenter.Update(_cameraViewProvider.GetView().Camera, _canvasRectTransform);
        }
    }

    public void Create(IUnitView unitView, HealthBarView healthBarViewPrefab,
        List<IHealthChangedHandler> healthChangedHandlers, List<IDeathHandler> deathHandlers)
    {
        var presenter = _healthBarFactory.Create(unitView, healthBarViewPrefab);
        healthChangedHandlers.Add(presenter);
        var deadUnitCollector = new DeadUnitCollector<HealthBarPresenter>(_presentersToDestroy, presenter);
        deathHandlers.Add(deadUnitCollector);
        _presenters.Add(presenter);
    }
}