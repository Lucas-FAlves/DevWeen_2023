using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cauldron : MonoBehaviour, IInteractable
{
    private GameObject[] slots;
    private int currentIndex = 0;
    private ItemSO[] cauldronItems;

    private PlayerInputActions playerInputActions;
    private bool playerIsNear = false;
    private PlayerInteraction player;

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float interactionRange = 2f;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        player = FindObjectOfType<PlayerInteraction>();

        slots = new GameObject[transform.childCount];
        cauldronItems = new ItemSO[transform.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = transform.GetChild(i).gameObject;
        }
    }

    private void OnEnable()
    {
        playerInputActions.Player.VirarCaldeirao.performed += EmptyCauldron;
    }

    private void OnDisable()
    {
        playerInputActions.Player.VirarCaldeirao.performed -= EmptyCauldron;
    }

    private void EmptyCauldron(InputAction.CallbackContext context)
    {
        if (player.IsHoldingItem)
            return;

        if (!playerIsNear)
            return;

        currentIndex = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<SpriteRenderer>().sprite = null;
            cauldronItems[i] = null;
        }
    }

    public bool PlaceItemOnCauldron(ItemSO item)
    {
        if (currentIndex >= cauldronItems.Length)
            return false;

        cauldronItems[currentIndex] = item;
        slots[currentIndex].GetComponent<SpriteRenderer>().sprite = cauldronItems[currentIndex].sprite;
        currentIndex++;
        return true;
    }
    private void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, interactionRange, playerMask);

        if(player == null)
            playerIsNear = false;
        else
            playerIsNear = true;
    }

    public void Interact(PlayerInteraction player)
    {
    }

    public ItemSO GetItemSO()
    {
        throw new System.NotImplementedException();
    }

    public void DestroyItem()
    {
        throw new System.NotImplementedException();
    }
}
