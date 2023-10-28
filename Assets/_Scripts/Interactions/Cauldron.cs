using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private GameObject[] slots;
    private int currentIndex = 0;
    private ItemSO[] cauldronItems;

    private void Awake()
    {
        slots = new GameObject[transform.childCount];
        cauldronItems = new ItemSO[transform.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = transform.GetChild(i).gameObject;
        }
    }

    public bool PlaceItemOnCauldron(ItemSO item)
    {
        if (currentIndex >= cauldronItems.Length)
            return false;

        cauldronItems[currentIndex] = item;
        slots[currentIndex].GetComponent<SpriteRenderer>().sprite = cauldronItems[currentIndex].sprite;
        Debug.Log(cauldronItems[currentIndex]);
        currentIndex++;
        return true;
    }

    public void EmptyCauldron()
    {
        currentIndex = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<SpriteRenderer>().sprite = null;
            cauldronItems[i] = null;
        }
    }
}
