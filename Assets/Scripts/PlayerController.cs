using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Jump parameters
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    // Physics logic
    private ContactFilter2D GroundContactFilter;
    private Rigidbody2D rb;
    private bool isGrounded => rb.IsTouching(GroundContactFilter);
    

    [SerializeField]
    private InputActionReference walk, jump;

    // Create initial rigid body
    void Start()
    {
        //Define rigid body
        rb = GetComponent<Rigidbody2D>();
        //Sets the parameters on the contact filter.
        GroundContactFilter.SetLayerMask(LayerMask.GetMask("Ground"));
        GroundContactFilter.SetNormalAngle(90f, 90f);
    }

    // Update is called once per frame
    void Update()
    {
        float moveDirection = walk.action.ReadValue<float>();
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);

        if (jump.action.ReadValue<float>() == 1 && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}
