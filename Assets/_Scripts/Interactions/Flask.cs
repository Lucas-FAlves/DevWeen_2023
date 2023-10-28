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
    private bool isFull = false;
    public bool IsFull => isFull;

    private PlayerInputActions playerInputActions;
    private PlayerInteraction player;

    private PotionSO[] allPotionsSO;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        player = FindObjectOfType<PlayerInteraction>();
        allPotionsSO = Resources.LoadAll<PotionSO>("PotionsSO");
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
            player.IsHoldingFlask = true;
            player.SetCurrentFlask(this);
        }
        else
        {
            isOnHand = false;
            transform.parent = null;
            transform.localPosition = player.transform.position;
            player.IsHoldingFlask = false;
            player.SetCurrentFlask(null);
        }
    }

    public void FillFlask()
    {
        if (isFull) return;

        isFull = true;
    }
}
