using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Potion")]
public class PotionSO : ScriptableObject
{
    public int id;
    public ItemSO[] ingredients;
    public float reqTime;
    public Sprite potionSprite;
    //public BaseEffect effect
}
