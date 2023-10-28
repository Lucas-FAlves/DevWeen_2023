using UnityEngine;

public interface IInteractable
{
    public void Interact(PlayerInteraction player);
    public ItemSO GetItemSO();
    public void DestroyItem();
}