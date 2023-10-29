using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flask : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO itemSO;
    [SerializeField] ItemSO wrongPotion;

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
            //Invoke entrega event
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
        }
        else
        {
            isOnHand = false;
            transform.parent = null;
            transform.localPosition = player.transform.position;
            player.IsHoldingFlask = false;
            player.SetCurrentFlask(null);
        }
        AudioManager.instance.PlaySound(itemSO.audioString);
    }

    public void FillFlask(PotionSO potionFilledSO)
    {
        //SetCurrentPotionSO,
        Debug.Log(potionFilledSO.name + "!!!!");
        currentPotionSO = potionFilledSO;
        if (isFull) return;
        isFull = true;
        if (potionFilledSO != null)
        {
            AudioManager.instance.PlaySound(potionFilledSO.audioString);
            rightPotion = true;
            sr.sprite = potionFilledSO.potionSprite;
        } 
        else sr.sprite = wrongPotion.sprite;

    }
}
