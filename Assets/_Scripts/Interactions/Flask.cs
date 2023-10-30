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

    private bool isOnHand = true;
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
    private void Start()
    {
        player.SetCurrentFlask(this);
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
        player.SetCurrentFlask(this);
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
            this.player.IsHoldingFlask = true;
            player.IsHoldingFlask = true;
            this.player.SetCurrentFlask(this);
            player.SetCurrentFlask(this);
            AudioManager.instance.PlaySound(itemSO.audioString);
        }
        else
        {
            isOnHand = false;
            transform.parent = null;
            transform.localPosition = player.transform.position;
            this.player.IsHoldingFlask = true;
            player.IsHoldingFlask = false;
            this.player.SetCurrentFlask(null);
            player.SetCurrentFlask(null);
        }
    }

    public void FillFlask(int id)
    {
        Debug.Log(id + " Fill FLask");
        currentPotionSO = wrongActualPotionSO;
        foreach (var potion in allPotionsSO)
        {
            if (potion.id == id)
            {
                currentPotionSO = potion;
                break;
            }            
        }

        if (isFull) return;
        isFull = true;

        if (currentPotionSO != wrongActualPotionSO)
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
