using System;
using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Jump parameters
    public float moveSpeed = 5f;
    public float jumpForce = 30f;
    public float jumpSquat = 0.5f;

    //Upgade values
    public float jumpCount = 2;
    public float jumpCooldown = 0;
    public float attackLvl = 2;

    public float maxJumps = 2;

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
        GroundContactFilter.SetNormalAngle(45f, 135f);
    }

    // Update is called once per frame
    void Update()
    {
        float moveDirection = walk.action.ReadValue<float>();
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);

        if (isGrounded && jumpCooldown <= 0)
        {
            jumpCount = maxJumps;
        }

        if (jumpCooldown > 0) {
            jumpCooldown -= Time.deltaTime;
        }

        if (jump.action.ReadValue<float>() == 1 && jumpCount > 0 && jumpCooldown <= 0)
        {
            jumpCooldown = jumpSquat;
            jumpCount--;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Death
        if (collision.gameObject.CompareTag("Hazard"))
        {
            SceneManager.LoadScene("Assets/Scenes/Death Screen.unity");
        }
    }
}
