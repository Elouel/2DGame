using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class MoveScript : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float jumpForce = 30f;

    public LayerMask whatIsGround;
    public Transform groundCheck;

    private bool isJumping = false;
    private bool isFacingRight = true;
    private bool isGrounded = false;
    private float groundedRadius = 0.2f;

    private Rigidbody2D ps;
    private PlayerScript player;

    private void Start()
    {
        this.ps = this.GetComponent<Rigidbody2D>();
        this.player = (PlayerScript)this.GetComponent(typeof(PlayerScript));
    }

    private void FixedUpdate()
    {
        this.isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this.groundedRadius, this.whatIsGround);

        var move = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), 0f).x * this.moveSpeed;

        this.transform.Translate(new Vector3(move, 0, 0));

        if (move > 0 && !this.isFacingRight)
        {
            this.Flip();
        }
        else if (move < 0 && this.isFacingRight)
        {
            this.Flip();
        }

        if (this.player.IsAlive && this.isJumping)
        {
            this.isJumping = false;
            this.ps.AddForce(Vector2.up * jumpForce);
        }
    }

    private void Update()
    {
        if (this.isGrounded && (CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetButtonDown("Jump")) && this.player.IsAlive)
        {
            this.isGrounded = false;
            this.isJumping = true;
        }

        if (!this.player.IsAlive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Flip()
    {
        this.isFacingRight = !this.isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
    }
}
