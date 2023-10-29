using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagicTrash : MonoBehaviour
{
    private PlayerInteraction player;
    [SerializeField] private float interactionRange;
    [SerializeField] private LayerMask playerMask;

    private PlayerInputActions playerInputActions;

    private bool playerIsNear;
    public bool PlayerIsNear => playerIsNear;
    private void Awake()
    {
        player = FindObjectOfType<PlayerInteraction>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }
    private void OnEnable()
    {
        playerInputActions.Player.Lixo.performed += Interact;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Lixo.performed -= Interact;
    }
    private void Interact(InputAction.CallbackContext context)
    {
        if (!playerIsNear) return;

        if(!player.IsHoldingItem) return;

        player.currentInteractable?.DestroyItem();
        player.SetCurentItem(null);

    }

    private void Update()
    {
        Collider2D playerCol = Physics2D.OverlapCircle(transform.position, interactionRange, playerMask);

        if (playerCol == null)
            playerIsNear = false;
        else
            playerIsNear = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
