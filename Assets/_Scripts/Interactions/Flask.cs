using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flask : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO itemSO;
    [SerializeField] ItemSO wrongPotion;
    [SerializeField] PotionSO wrongActualPotionSO;

    private PotionSO currentPotionSO;
    public PotionSO CurrentPotionSO => currentPotionSO;

    private bool isOnHand = false;
    private SpriteRenderer sr;
    public bool IsOnHand => isOnHand;
    private bool isFull = false;
    public bool IsFull => isFull;

    private bool rightPotion = false;
    public bool RightPotion => rightPotion;

    private PlayerInputActions playerInputActions;
    private PlayerInteraction player;

    private PotionSO[] allPotionsSO;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        player = FindObjectOfType<PlayerInteraction>();
        allPotionsSO = Resources.LoadAll<PotionSO>("PotionsSO");
        sr = GetComponent<SpriteRenderer>();
    }

    public void DestroyItem()
    {
        var trash = FindObjectOfType<MagicTrash>();
        if (trash.PlayerIsNear)
        {
            isFull = false;
            sr.sprite = itemSO.sprite;
        } 
        else
        {
            isFull = false;
            sr.sprite = itemSO.sprite;
            Destroy(gameObject);
        }
    }

    public ItemSO GetItemSO()
    {
        return itemSO;
    }

    public void Interact(PlayerInteraction player)
    {
        var trash = FindObjectOfType<MagicTrash>();
        if (trash.PlayerIsNear)
            return;

        if (!isOnHand && !player.IsHoldingItem)
        {
            isOnHand = true;
            transform.parent = player.ItemHolderTransform;
            transform.localPosition = Vector3.zero;
            player.IsHoldingFlask = true;
            player.SetCurrentFlask(this);
            AudioManager.instance.PlaySound(itemSO.audioString);
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

    public void FillFlask(int id)
    {
        //SetCurrentPotionSO,
        foreach(var potion in allPotionsSO)
        {
            if (potion.id == id)
            {
                currentPotionSO = potion;
                break;
            }            
        }

        currentPotionSO = wrongActualPotionSO;

        if (isFull) return;
        isFull = true;
        if (currentPotionSO != null)
        {
            AudioManager.instance.PlaySound(currentPotionSO.audioString);
            rightPotion = true;
            sr.sprite = currentPotionSO.potionSprite;
        }
        else
        {
            AudioManager.instance.PlaySound("falha");
            sr.sprite = wrongPotion.sprite;
        }

    }
}
