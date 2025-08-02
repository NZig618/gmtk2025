using System;
using System.Collections;
using System.Runtime.InteropServices;
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
    public float jumpCooldown = 0f;

    // Attack parameters
    public float gunHeat = 0.25f;
    public float gunCooloown = 0f;

    //Upgade values
    public float jumpCount = 2;
    public float attackLvl = 2;

    public float maxJumps = 2;

    // Physics logic
    private ContactFilter2D GroundContactFilter;
    private Rigidbody2D rb;
    private bool IsGrounded => rb.IsTouching(GroundContactFilter);


    [SerializeField]
    private InputActionReference walk, jump, attack;

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

        if (jumpCooldown > 0)
        {
            jumpCooldown -= Time.deltaTime;
        }

        if (gunCooloown > 0)
        {
            gunCooloown -= Time.deltaTime;
        }

        if (IsGrounded && jumpCooldown <= 0)
        {
            jumpCount = maxJumps;
        }

        if (jump.action.ReadValue<float>() == 1 && jumpCount > 0 && jumpCooldown <= 0)
        {
            jumpCooldown = jumpSquat;
            jumpCount--;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (attack.action.ReadValue<float>() == 1 && gunCooloown <= 0)
        {
            gunCooloown = gunHeat;
            Fire();
        }
    }

    public GameObject bulletLvl1;

    public GameObject bulletLvl2;

    public Transform firePoint;
    public float fireForce = 20f;

    void Fire()
    {
        if (attackLvl == 1)
        {
            GameObject bullet = Instantiate(bulletLvl1, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }
        else if (attackLvl >= 2)
        {
            GameObject bullet = Instantiate(bulletLvl2, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Death
        if (collision.gameObject.CompareTag("Hazard"))
        {
            fadeAnim.Play("FadeToBlack");
            StartCoroutine(DelayPlayerFade("Assets/Scenes/Death Screen.unity"));
        }
    }

    public Animator fadeAnim;
    public float fadeTime = 1f;

    IEnumerator DelayPlayerFade(String scene)
    {
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(scene);
    }

    public UpgradeManager upgrader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
            upgrader.upgradeCount++;
        }
    }
}
