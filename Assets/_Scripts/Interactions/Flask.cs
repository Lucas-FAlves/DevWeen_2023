using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flask : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO itemSO;
    private bool isOnHand = false;
    public bool IsOnHand => isOnHand;

    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    public ItemSO GetItemSO()
    {
        return itemSO;
    }

    public void Interact(PlayerInteraction player)
    {
        if (!isOnHand && !player.IsHoldingItem)
        {
            isOnHand = true;
            transform.parent = player.ItemHolderTransform;
            transform.localPosition = Vector3.zero;
        }
        else
        {
            isOnHand = false;
            transform.parent = null;
            transform.localPosition = player.transform.position;
        }
    }
}
