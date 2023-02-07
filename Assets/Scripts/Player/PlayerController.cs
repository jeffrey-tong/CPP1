using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    //Components
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    public float speed;
    public float jumpForce;

    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius;

    Coroutine jumpForceChange;
    Coroutine bigChange;

    public int maxLives = 5;
    private int _lives = 3;
    public int lives
    {
        get { return _lives; }
        set
        {
            //if(_lives > value)
            //lost a life need to respawn

            _lives = value;
            if(_lives > maxLives)
            {
                _lives = maxLives;
            }

            //if(_lives < 0)
            //gameover
            Debug.Log("Lives have been set to: " + lives.ToString());
        }
    }

    private int _score;
    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            Debug.Log("Score: " + score.ToString());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if(speed <= 0)
        {
            speed = 6.0f;
            Debug.Log("Speed was set incorrectly, defaulting to " + speed.ToString());
        }
        if(jumpForce <= 0)
        {
            jumpForce = 300.0f;
            Debug.Log("Jump Force was set incorrectly, defaulting to " + jumpForce.ToString());
        }
        if(groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
            Debug.Log("Ground Check Radius was set incorrectly, defaulting to " + groundCheckRadius.ToString());
        }

        if(!groundCheck)
        {
            groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
            Debug.Log("Ground Check not set, finding it manually");
        }
    }

    // Update is called once per frame
    void Update(){
        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxisRaw("Horizontal");
       
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if(curPlayingClip.Length > 0)
        {
            if(Input.GetButtonDown("Fire1") && curPlayingClip[0].clip.name != "Fire"){
                anim.SetTrigger("fire");
            }
            else if(curPlayingClip[0].clip.name == "Fire"){
                rb.velocity = Vector2.zero;
            }
            else{
                Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
        }

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        if(!isGrounded && Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("JumpAttack");
        }

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);

        //check for flipped and create an algorithm to flip character
        if(hInput != 0)
        {
            sr.flipX = hInput < 0;
        }

        if(isGrounded)
        {
            rb.gravityScale = 1;
        }
    }

    public void IncreaseGravity()
    {
        rb.gravityScale = 5;
    }

    public void StartJumpForceChange()
    {
        if(jumpForceChange == null)
        {
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
        else
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce /= 2;
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
    }

    IEnumerator JumpForceChange()
    {
        jumpForce *= 2;

        yield return new WaitForSeconds(5.0f);

        jumpForce /= 2;
        jumpForceChange = null;
    }

    public void StartBigChange()
    {
        if(bigChange == null)
        {
            bigChange = StartCoroutine(BigChange());
        }
        else
        {
            StopCoroutine(bigChange);
            bigChange = null;
            this.transform.localScale /= 2;
            bigChange = StartCoroutine(BigChange());
        }
    }

    IEnumerator BigChange()
    {
        this.transform.localScale *= 2;

        yield return new WaitForSeconds(5.0f);

        this.transform.localScale /= 2;
        bigChange = null;
    }
}
