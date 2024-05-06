using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class King : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public bool doorColliderBool = false;
    private GameObject door;
    public BoxCollider2D doorCollider;
    public int isPaused;
    public King kingScript;

    [Header("Movement")]
    public float speed = 3f;
    private float moveInput;
    private bool facingRight = true;
    public float jumpForce = 7f;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    [Header("Ghost")]
    public GameObject ghost;
    public GameObject deathEffect;
    private Animator ghostAnim;

    // Для сохранения состояния анимации
    private AnimatorStateInfo ghostCurrentState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        door = GameObject.FindGameObjectWithTag("Door");
        doorCollider = door.GetComponent<BoxCollider2D>();
        ghostAnim = ghost.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Walk();

        if (!facingRight && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight && moveInput < 0)
        {
            Flip();
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        anim.SetBool("isRunning", moveInput != 0);
        anim.SetBool("isJumping", Input.GetKeyDown(KeyCode.W) && isGrounded);
    }

    void Walk()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Spike"))
        {
            ghostCurrentState = ghostAnim.GetCurrentAnimatorStateInfo(0);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            ghost.SetActive(true);
            ghost.transform.position = transform.position;
            ghostAnim.Play(ghostCurrentState.fullPathHash, 0, ghostCurrentState.normalizedTime);
        }

        if (collider.CompareTag("Door"))
        {
            UnlockLevel();
            doorColliderBool = true;
            doorCollider.enabled = false;
            isPaused = 1;
            StartCoroutine(kingStop(0.5f));
        }

        if (collider.CompareTag("MovementPlatform"))
        {
            transform.SetParent(collider.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("MovementPlatform"))
        {
            transform.SetParent(null);
        }
    }

    public void UnlockLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel >= PlayerPrefs.GetInt("levels"))
        {
            PlayerPrefs.SetInt("levels", currentLevel + 1);
        }
    }

    IEnumerator kingStop(float time)
    {
        yield return new WaitForSeconds(time);
        kingScript.enabled = false;
        anim.speed = 0.0f;
        rb.Sleep();
    }
}