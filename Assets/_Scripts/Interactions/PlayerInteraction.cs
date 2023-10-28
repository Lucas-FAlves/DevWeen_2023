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
    [SerializeField] private LayerMask cauldronMask;
    [SerializeField] private Transform interactionPoint;
    public Transform ItemHolderTransform;

    private PlayerInputActions inputActions;

    private IInteractable currentInteractable;
    private bool isHoldingItem;
    public bool IsHoldingItem => isHoldingItem && currentInteractable != null;

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

        if (isHoldingItem)
        {
            if(CheckForCauldron())
            {
                var cauldron = Physics2D.OverlapCircle(interactionPoint.position, interactionRange, cauldronMask);
                bool placed = cauldron.GetComponent<Cauldron>().PlaceItemOnCauldron(currentInteractable.GetItemSO());

                if (!placed) return;

                currentInteractable.DestroyItem();
                currentInteractable = null;
                isHoldingItem = false;
                return;
            } 
            else
            {
                currentInteractable?.Interact(this);
                isHoldingItem = false;
                return;
            }
            
        }

        foreach (var collider in collider2Ds)
        {
            isHoldingItem = true;
            currentInteractable = collider2Ds[0].GetComponent<IInteractable>();
            currentInteractable?.Interact(this);
        }
    }

    public void SetCurentItem(IInteractable interactable)
    {
        currentInteractable = interactable;
    }

    private bool CheckForCauldron()
    {
        var cauldron = Physics2D.OverlapCircle(interactionPoint.position, interactionRange, cauldronMask);
        return (cauldron == null) ? false : true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
