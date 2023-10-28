using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{   
    public InputAction playerControls;
    public float moveSpeed;
    private Rigidbody2D _rigidbody;
    private Transform _playerTransform;
    Vector2 movementValue;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _playerTransform = gameObject.transform;
        playerControls.Enable();
    }

    // Update is called once per frame
    void Update()
    {   
        //int moveUp, moveDown, moveLeft, moveRight;
        movementValue = playerControls.ReadValue<Vector2>();

        // if(movementValue.x != 0)
        // {
        //     if (movementValue.x > 0)
        //     {
        //         moveRight = 1;
        //     }
        //     else
        //     {
        //         moveLeft = 1;
        //     }
        // }
        // if(movementValue.y != 0)
        // {
        //     if (movementValue.y > 0)
        //     {
        //         moveUp = 1;
        //     }
        //     else
        //     {
        //         moveDown = 1;
        //     }
        // }
   
    }

    private void FixedUpdate() 
    {
        _rigidbody.velocity = movementValue * moveSpeed;
    }


}
