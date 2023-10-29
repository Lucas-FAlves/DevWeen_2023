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

    public IInteractable currentInteractable;
    private Flask currentFlask = null;
    private bool isHoldingItem;
    public bool IsHoldingItem => isHoldingItem && currentInteractable != null;

    [SerializeField] private ItemSO flaskSO;

    [HideInInspector] public bool IsHoldingFlask = false;

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
        Collider2D collider2Ds = Physics2D.OverlapCircle(interactionPoint.position, interactionRange, interactionMask);

        if (isHoldingItem)
        {
            if(CheckForCauldron())
            {
                var cauldron = Physics2D.OverlapCircle(interactionPoint.position, interactionRange, cauldronMask);
                bool placed = cauldron.GetComponent<Cauldron>().PlaceItemOnCauldron(currentInteractable?.GetItemSO());

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
                currentInteractable = null;
                return;
            }
            
        }

        if (collider2Ds == null)
            return;

        currentInteractable = collider2Ds.GetComponent<IInteractable>();
        currentInteractable?.Interact(this);
        isHoldingItem = true;
    }
    

    public void SetCurentItem(IInteractable interactable)
    {
        currentInteractable = interactable;
    }

    public ItemSO GetItemSO()
    {
        if (currentInteractable == null) return null;

        return currentInteractable?.GetItemSO();
    }

    public void SetCurrentFlask(Flask flask)
    {
        currentFlask = flask;
    }
    public Flask GetCurrentFlask()
    {
        return currentFlask;
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
