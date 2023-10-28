using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Ingredient : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO itemSO;

    private bool isOnHand = false;
    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
    public void Interact(GameObject player)
    {
        if (!isOnHand)
        {
            isOnHand = true;

        }
        Debug.Log(itemSO.id);
    }

}
