using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Ingredient : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO itemSO;
    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
    public void Interact()
    {
        Debug.Log(itemSO.id);
    }

}
