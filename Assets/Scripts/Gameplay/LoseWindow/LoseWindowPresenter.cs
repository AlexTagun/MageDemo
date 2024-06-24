using UnityEngine;
using Zenject;

public class LoseWindowPresenter : IStart, IDeathHandler
{
    private readonly Canvas _canvas;
    private readonly LoseWindow _loseWindowPrefab;
    private readonly SceneLoadService _sceneLoadService;

    private LoseWindow _loseWindow;

    [Inject]
    public LoseWindowPresenter(UiBindings uiBindings, LoseWindow loseWindowPrefab, SceneLoadService sceneLoadService)
    {
        _canvas = uiBindings.Canvas;
        _loseWindowPrefab = loseWindowPrefab;
        _sceneLoadService = sceneLoadService;
    }

    void IStart.Start()
    {
        _loseWindow = Object.Instantiate(_loseWindowPrefab, _canvas.transform);
        _loseWindow.Button.onClick.AddListener(OnButtonClick);
    }

    public void OnDeath(IUnitView source)
    {
        _loseWindow.Show();
    }

    private void OnButtonClick()
    {
        _sceneLoadService.LoadGameplayScene();
    }
}