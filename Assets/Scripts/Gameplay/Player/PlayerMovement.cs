using UnityEngine;

public class PlayerMovement
{
    private readonly IMovementInput _movementInput;
    private readonly PlayerView _view;
    private readonly PlayerSettings _playerSettings;

    public PlayerMovement(IMovementInput movementInput, PlayerView view, PlayerSettings playerSettings)
    {
        _movementInput = movementInput;
        _view = view;
        _playerSettings = playerSettings;
    }

    public void Update()
    {
        var movement = _movementInput.GetMovement();
        var velocity = _playerSettings.MovementSpeed * new Vector3(movement.x, 0, movement.y);
        _view.SetVelocity(velocity);

        if (velocity.magnitude > 0)
        {
            _view.transform.rotation = Quaternion.LookRotation(velocity);
        }
    }
}