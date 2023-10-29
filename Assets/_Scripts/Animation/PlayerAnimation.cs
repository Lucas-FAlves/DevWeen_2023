using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private Animator animator;
    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputActions.Player.Move.performed += ChangeAnimation;
    }
    private void OnDisable()
    {
        inputActions.Player.Move.performed -= ChangeAnimation;
    }

    private void ChangeAnimation(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();
        animator.SetFloat("xInput", direction.x);
        animator.SetFloat("yInput", direction.y);
    }
}
