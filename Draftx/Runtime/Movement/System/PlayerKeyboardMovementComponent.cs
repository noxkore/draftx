using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardMovementInput : BaseMovementInputComponent
{
    protected override Vector2 ReadDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        return new Vector2(x, y);
    }
}

