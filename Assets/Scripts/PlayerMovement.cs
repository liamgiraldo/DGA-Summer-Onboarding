using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool doubleJumpAvailable = false;

    private bool isGroundPounding = false;
    private float gpCooldown = 5f;
    private float nextGp = 0f;

    [SerializeField] public Animator animator;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float doubleJumpFactor;
    [SerializeField] private float rotationFactor;

    [SerializeField] private float rotationBound;

    [SerializeField] private Balloon balloon;

    [SerializeField] private TMP_Text gpCooldownText;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f && !isGroundPounding)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKey(KeyCode.LeftShift) && !IsGrounded() && !isGroundPounding && !balloon.isBalloonActive && Time.time > nextGp)
        {
            rb.velocity = new Vector2(rb.velocity.x, -jumpingPower);
            nextGp = Time.time + gpCooldown;
            isGroundPounding = true;
        }

        if (IsGrounded() && isGroundPounding)
        {
            doubleJumpAvailable = true;
            isGroundPounding = false;
            rb.SetRotation(0);
        }
        else if (IsGrounded()){
            doubleJumpAvailable = true;
            rb.SetRotation(0);
        }

        if(Input.GetButtonDown("Jump") && !IsGrounded() && doubleJumpAvailable){
            rb.velocity = new Vector2(rb.velocity.x, doubleJumpFactor);
            doubleJumpAvailable = false;
        }

        if (Time.time < nextGp)
        {
            gpCooldownText.text = ((int)(nextGp - Time.time) + 1).ToString();
        }
        else
        {
            gpCooldownText.text = 0.ToString();
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Lethal")){
            GameManager.instance.isGameWon = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
