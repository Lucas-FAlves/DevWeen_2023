using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private Animator animator;
    private Vector2 lastDir;
    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputActions.Player.Move.performed += ChangeAnimation;
        inputActions.Player.Move.canceled += CancelAnimation;
    }
    private void OnDisable()
    {
        inputActions.Player.Move.performed -= ChangeAnimation;
        inputActions.Player.Move.canceled -= CancelAnimation;
    }

    private void CancelAnimation(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();
        animator.SetFloat("xInputStop", direction.x);
        animator.SetFloat("yInputStop", direction.y);
    }

    private void ChangeAnimation(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();
        lastDir = direction;
        animator.enabled = true;
        if (direction == Vector2.zero)
        {
            animator.enabled = false;
            return;
        }

        animator.SetFloat("xInput", direction.x);
        animator.SetFloat("yInput", direction.y);
    }
}
