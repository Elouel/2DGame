using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class MoveScript : MonoBehaviour
{
    public float moveSpeed = 0.05f;
    public float jumpForce = 350f;

    public LayerMask whatIsGround;
    public Transform groundCheck;

    private bool isJumping = false;
    private bool isFacingRight = true;
    private bool isGrounded = false;
    private bool isAlive = true;

    private float groundedRadius = 0.2f;

    private Rigidbody2D ps;
    private Animator anim;

    public void Start()
    {
        this.ps = this.GetComponent<Rigidbody2D>();
        this.anim = this.GetComponent<Animator>();
    }

    //public void FixedUpdate()
    //{
        


    //    if (this.IsAlive && this.isJumping)
    //    {
    //        this.isJumping = false;
    //        Vector2 force = Vector2.up * this.jumpForce;
    //        var moveY = this.ps.velocity.y;
    //        moveY += force.y;
    //        this.ps.velocity = new Vector2(0, moveY);
    //    }
    //}

    public void Update()
    {
        if (!this.IsAlive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        this.isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this.groundedRadius, this.whatIsGround);

        if (this.isGrounded && (CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetButtonDown("Jump")) && this.isAlive)
        {
            this.anim.SetTrigger("isJumping");
            this.isJumping = true;
        }
        
        var move = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), 0f).x * this.moveSpeed * Time.deltaTime;
        this.transform.Translate(new Vector3(move, 0, 0));

        if (this.IsAlive && this.isJumping)
        {
            this.isJumping = false;
            Vector2 force = Vector2.up * this.jumpForce;
            var moveY = this.ps.velocity.y;
            moveY += force.y;
            this.ps.velocity = new Vector2(0, moveY);
        }

        if (move > 0 && !this.isFacingRight)
        {
            this.Flip();
        }
        else if (move < 0 && this.isFacingRight)
        {
            this.Flip();
        }
    }

    private void Flip()
    {
        this.isFacingRight = !this.isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
    }

    public bool IsAlive
    {
        get
        {
            return this.isAlive;
        }
        set
        {
            this.isAlive = value;
        }
    }
}
