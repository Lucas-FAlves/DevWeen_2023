using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{   
    public InputAction playerControls;
    public float moveSpeed;
    [HideInInspector] public float auxSpeed;
    private Rigidbody2D _rigidbody;
    Vector2 movementValue;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        auxSpeed = moveSpeed;
        playerControls.Enable();
    }

    // Update is called once per frame
    void Update()
    {   
        //int moveUp, moveDown, moveLeft, moveRight;
        movementValue = playerControls.ReadValue<Vector2>();   
    }

    private void FixedUpdate() 
    {
        _rigidbody.velocity = movementValue * moveSpeed;
    }
}
