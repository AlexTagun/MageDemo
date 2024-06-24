using UnityEngine;

public class HealthBarPresenter : IHealthChangedHandler
{
    private readonly IUnitView _unitView;
    private readonly HealthBarView _view;
    private readonly RectTransform _rectTransform;

    public HealthBarPresenter(IUnitView unitView, HealthBarView view)
    {
        _unitView = unitView;
        _view = view;
        _rectTransform = view.transform as RectTransform;
    }

    public void Update(Camera camera, RectTransform canvasRectTransform)
    {
        Vector2 viewportPosition = camera.WorldToViewportPoint(_unitView.GetPosition());
        var canvasSizeDelta = canvasRectTransform.sizeDelta;
        var screenPosition = new Vector2(
            viewportPosition.x * canvasSizeDelta.x - canvasSizeDelta.x * 0.5f,
            viewportPosition.y * canvasSizeDelta.y - canvasSizeDelta.y * 0.5f);
        var offset = new Vector2(0, _unitView.GetHealthBarOffsetY());
        _rectTransform.anchoredPosition = screenPosition + offset;
    }

    public void OnHealthChanged(HealthChangedContext ctx)
    {
        _view.SetHealth(ctx.NewHealthPercentage);
    }

    public void Destroy()
    {
        Object.Destroy(_view.gameObject);
    }
}