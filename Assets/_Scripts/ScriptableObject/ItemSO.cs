using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Item")]
public class ItemSO : ScriptableObject
{
    public int id;
    public Animator anim;
    public Sprite sprite;
    public string audioString;
}
