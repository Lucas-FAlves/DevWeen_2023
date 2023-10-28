using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private LayerMask interactionMask;
    [SerializeField] private Transform interactionPoint;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }

    private void OnEnable()
    {
        inputActions.Player.Interact.performed += CheckForInteractions;
    }
    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= CheckForInteractions;
    }
    private void CheckForInteractions(InputAction.CallbackContext context)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(interactionPoint.position, interactionRange, interactionMask);

        foreach (var collider in collider2Ds)
        {
            collider.GetComponent<IInteractable>()?.Interact();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
