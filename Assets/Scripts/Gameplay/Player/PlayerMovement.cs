using UnityEngine;
using Zenject;

public class PlayerMovement : IUpdate
{
    private readonly IMovementInput _movementInput;
    private readonly PlayerViewProvider _playerViewProvider;
    private readonly PlayerSettings _settings;

    [Inject]
    public PlayerMovement(IMovementInput movementInput, PlayerViewProvider playerViewProvider,
        GameplaySettings gameplaySettings)
    {
        _movementInput = movementInput;
        _playerViewProvider = playerViewProvider;
        _settings = gameplaySettings.PlayerSettings;
    }

    void IUpdate.Update()
    {
        var view = _playerViewProvider.GetView();
        var movement = _movementInput.GetMovement();
        var velocity = _settings.MovementSpeed * new Vector3(movement.x, 0, movement.y);
        view.SetVelocity(velocity);
        view.SetAngularVelocity(Vector3.zero);

        if (velocity.magnitude > 0)
        {
            view.transform.rotation = Quaternion.LookRotation(velocity);
        }
    }
}