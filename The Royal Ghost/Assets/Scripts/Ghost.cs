using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    public float speed = 3f;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private bool facingRight = true;

    public GameObject king;
    public GameObject lifeEffect;
    public GameObject resurrection;
    public GameObject gameOverText;
    private Animator kingAnim;

    private bool hasColPlat = false;

    // ƒл€ сохранени€ состо€ни€ анимации корол€
    private AnimatorStateInfo kingCurrentState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        kingAnim = king.GetComponent<Animator>();
    }

    private void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        anim.SetBool("isFlying", moveInput.x != 0 || moveInput.y != 0);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * speed * Time.fixedDeltaTime);

        if (!facingRight && moveInput.x > 0)
        {
            Flip();
        }
        else if (facingRight && moveInput.x < 0)
        {
            Flip();
        }

        if (gameObject.activeInHierarchy && !resurrection.activeInHierarchy)
        {
            GameOver();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Platform"))
        {
            hasColPlat = true;
        }

        if (collider.CompareTag("Aegis") && !hasColPlat)
        {
            kingCurrentState = kingAnim.GetCurrentAnimatorStateInfo(0);
            Instantiate(lifeEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            king.SetActive(true);
            king.transform.position = transform.position;
            resurrection.SetActive(false);

            // ¬осстанавливаем анимацию корол€ с сохраненного состо€ни€
            kingAnim.Play(kingCurrentState.fullPathHash, 0, kingCurrentState.normalizedTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            hasColPlat = false;
        }
    }

    private void GameOver()
    {
        gameOverText.SetActive(true);
    }
}
