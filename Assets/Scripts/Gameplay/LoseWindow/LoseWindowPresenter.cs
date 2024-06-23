using UnityEngine;
using Zenject;

public class LoseWindowPresenter : IStart, IDeath
{
    [Inject] private LoseWindow _loseWindowPrefab;
    [Inject] private SceneLoadService _sceneLoadService;

    private LoseWindow _loseWindow;

    void IStart.Start()
    {
        var canvas = Object.FindObjectOfType<Canvas>();

        _loseWindow = Object.Instantiate(_loseWindowPrefab, canvas.transform);
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