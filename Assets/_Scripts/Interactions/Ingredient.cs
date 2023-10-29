using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Ingredient : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO itemSO;

    private bool isOnHand = true;
    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    private void Start()
    {
        AudioManager.instance.PlaySound(itemSO.audioString);
    }
    public void Interact(PlayerInteraction player)
    {
        if (!isOnHand && !player.IsHoldingItem)
        {
            isOnHand = true;
            transform.parent = player.ItemHolderTransform;
            transform.localPosition = Vector3.zero;
            AudioManager.instance.PlaySound(itemSO.audioString);
        } 
        else
        {
            isOnHand = false;
            transform.parent = null;
            transform.localPosition = player.transform.position;
        }
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    public ItemSO GetItemSO()
    {
        return itemSO;
    }
}
