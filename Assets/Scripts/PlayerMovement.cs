using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public bool isAbleMove = true;

    public float PlayerSpeed = 5f;
    [Range(1, 10)]
    public float JumpSpeed = 5f;
    public bool IsGrounded; //condition for jump
    public Transform GroundCheck;//check point

    public LayerMask Ground;//Layer
    public float FallAcc = 3.2f; //acceleration fall
    public float JumpAcc = 2.1f; //acceleration jump

    public int JumpCount = 2; //for 2-times jump

    //public ParticleSystem PlayerPS;//particle

    private float moveX;
    private bool moveJump;
    private bool jumpHold;//holding to jump
    private bool isJump; //2-times jump



    private enum playerState { idle, run };

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame---input
    void Update()
    {
        moveX = Input.GetAxis("Horizontal"); //-1~1
        moveJump = Input.GetButtonDown("Jump");
        jumpHold = Input.GetButton("Jump");

        if (moveJump && JumpCount > 0)
        {
            isJump = true;
            //PPS();
        }
    }

    //Physics
    private void FixedUpdate()
    {

        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, Ground);
        if (isAbleMove)
        {
            Move();
            Jump();
            playerAnim();
        }
    }

    //Movement
    private void Move()
    {
        //anim.SetFloat("speed",Mathf.Abs(moveX));
        //moveX = Input.GetAxis("Horizontal"); //-1~1
        rb.velocity = new Vector2(moveX * PlayerSpeed, rb.velocity.y);

        //Flip
        if (moveX != 0)
        {
            if (moveX >= 0)
            {
                //PPS();
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                //PPS();
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

    }

    private void Jump()
    {
        if (IsGrounded)
        {
            JumpCount = 2;
        }

        if (isJump)
        {
            rb.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);
            JumpCount--;
            isJump = false;
        }

        if (rb.velocity.y < 0) //falling
        {
            rb.gravityScale = FallAcc;
            //rb.velocity += Vector2.up * Physics2D.gravity.y * (FallAcc-1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && (!jumpHold))//jumping
        {
            rb.gravityScale = JumpAcc;
            //rb.velocity += Vector2.up * Physics2D.gravity.y * (JumpAcc - 1) * Time.fixedDeltaTime;
        }
        else
        {
            rb.gravityScale = 1f;
        }

    }
    /*
    void PPS()//particle
    {
        PlayerPS.Play();
    }
    */

    void playerAnim()
    {
        playerState states;
        if (Mathf.Abs(moveX) > 0)
        {
            states = playerState.run;
        }
        else
        {
            states = playerState.idle;
        }
        /*
        if (rb.velocity.y > 0.1f)
        {
            states = playerState.jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            states = playerState.fall;
        }
        */
        anim.SetInteger("state", (int)states);
        
    }

}
