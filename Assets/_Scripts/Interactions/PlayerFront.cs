using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFront : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }
    private void OnEnable()
    {
        inputActions.Player.Move.performed += Move;
    }
    private void OnDisable()
    {
        inputActions.Player.Move.performed -= Move;
    }

    private void Move(InputAction.CallbackContext context)
    {
        var vet2 = context.ReadValue<Vector2>();
        if (vet2 == Vector2.zero)
            return;

        transform.localPosition = new Vector3(vet2.x, vet2.y, 0f);
    }
}
