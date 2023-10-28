using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFront : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }
    private void OnEnable()
    {
       
    }
}
