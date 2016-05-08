using System.Collections.Generic;
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
    private bool isAlive = true;

    private float groundedRadius = 0.2f;

    private Rigidbody2D ps;
    private Animator anim;
    private List<Touch> touches;


    public void Start()
    {
        this.ps = this.GetComponent<Rigidbody2D>();
        this.anim = this.GetComponent<Animator>();
        this.touches = new List<Touch>();
    }

    public void FixedUpdate()
    {
        this.isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this.groundedRadius, this.whatIsGround);

        //this.transform.Translate(new Vector3(move, this.ps.velocity.y, 0) * Time.deltaTime);

        Debug.Log(CrossPlatformInputManager.GetAxis("Horizontal"));
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

        if (this.IsAlive && this.isJumping)
        {
            this.isJumping = false;
            this.ps.AddForce(Vector2.up * jumpForce);
        }
    }

    public void Update()
    {
		if (this.isGrounded && ( CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetButtonDown("Jump") ) && this.isAlive)
        {
            this.isGrounded = false;
            this.isJumping = true;
            //this.anim.Play("Jump");
        }

        if (!this.IsAlive)
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
