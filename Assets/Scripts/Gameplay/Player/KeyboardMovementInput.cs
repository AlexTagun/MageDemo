using UnityEngine;

public class KeyboardMovementInput : IMovementInput
{
    Vector2 IMovementInput.GetMovement()
    {
        var movement = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x -= 1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement.x += 1;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement.y += 1;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement.y -= 1;
        }

        return movement.normalized;
    }
}