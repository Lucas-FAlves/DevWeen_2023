using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject itemPrefab;

    public void DestroyItem()
    {
        
    }

    public ItemSO GetItemSO()
    {
        return null;
    }

    public void Interact(PlayerInteraction player)
    {
        var clone = Instantiate(itemPrefab, player.ItemHolderTransform);
        clone.transform.parent = player.ItemHolderTransform;
        clone.transform.localPosition = Vector3.zero;
        player.SetCurentItem(clone.GetComponent<IInteractable>());
    }
}
