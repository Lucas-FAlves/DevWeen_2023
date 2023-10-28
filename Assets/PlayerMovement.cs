using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{   
    public InputAction playerControls;
    public float moveSpeed;
    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerControls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementValue = playerControls.ReadValue<Vector2>();
        _rigidbody.velocity = movementValue * moveSpeed;
    }
}
