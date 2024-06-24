using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class HealthBarFactory
{
    private readonly RectTransform _root;

    [Inject]
    public HealthBarFactory(UiBindings uiBindings)
    {
        _root = uiBindings.HealthBarRoot;
    }

    public HealthBarPresenter Create(IUnitView unitView, HealthBarView healthBarViewPrefab)
    {
        var healthBarView = Object.Instantiate(healthBarViewPrefab, _root);
        return new HealthBarPresenter(unitView, healthBarView);
    }
}