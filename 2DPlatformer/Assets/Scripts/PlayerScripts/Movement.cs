using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private PlayerCollisions coll;
    private PlayerDash dash;

    [SerializeField]
    private float f_speed = 15f;
    [SerializeField][Range(1,20)]
    private float f_jumpForce;
    [SerializeField]
    private float f_slideSpeed = 2.2f;

    [Space]
    public float f_fallMultiplier = 2.5f;
    public float f_lowJumpMultiplier = 2f;
    public float wallJumpLerp = 10f;

    private Rigidbody2D rb;

    [Header("Bools")]
    private bool wallGrab;
    private bool canMove;
    private bool wallJumped;

    public SpriteRenderer playerSprite;

    [SerializeField]
    float fJumpVelocity = 5;


    float fJumpPressedRemember = 0;
    [SerializeField]
    float fJumpPressedRememberTime = 0.2f;

    float fGroundedRemember = 0;
    [SerializeField]
    float fGroundedRememberTime = 0.25f;

    [SerializeField]
    float fHorizontalAcceleration = 1;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingBasic = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenStopping = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenTurning = 0.5f;

    [SerializeField]
    [Range(0, 1)]
    float fCutJumpHeight = 0.5f;

    void Start()
    {
        coll = GetComponent<PlayerCollisions>();
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(x, y);

        Walk(dir);


        wallGrab = coll.onWall && Input.GetKey(KeyCode.LeftShift);

        if (wallGrab)
        {
            rb.velocity = new Vector2(rb.velocity.x, y * f_speed);
        }

        if(coll.onWall && !coll.onGround && !Input.GetKey(KeyCode.LeftShift))
        {
            WallSlide();
        }

        if(!wallJumped)
        {
            Debug.Log("!wallJumped");
            rb.velocity = new Vector2(dir.x * f_speed, rb.velocity.y);
        }
        else
        {
            Debug.Log("Walljumped");
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * f_speed, rb.velocity.y)), .5f * Time.deltaTime);
        }

        fGroundedRemember -= Time.deltaTime;

        if (coll.onGround)
        {
            fGroundedRemember = fGroundedRememberTime;
        }

        fJumpPressedRemember -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fJumpPressedRemember = fJumpPressedRememberTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * fCutJumpHeight);
            }
        }

        if ((fJumpPressedRemember > 0) && (fGroundedRemember > 0))
        {
            fJumpPressedRemember = 0;
            fGroundedRemember = 0;
            rb.velocity = new Vector2(rb.velocity.x, fJumpVelocity);
        }

        float fHorizontalVelocity = rb.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * 10f);
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity))
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.deltaTime * 10f);
        else
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime * 10f);

        rb.velocity = new Vector2(fHorizontalVelocity, rb.velocity.y);
    }





    private void Walk(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * f_speed, rb.velocity.y));     

    }

    /*private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && (coll.onGround))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * f_cutJumpHeight);
        }

        /*else if(Input.GetKeyDown(KeyCode.Space) && (coll.onLeftWall))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * f_jumpForce);
            Debug.Log("LeftWall");
        }

        else if (Input.GetKeyDown(KeyCode.Space) && (coll.onRightWall))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * f_jumpForce);
            Debug.Log("RightWall");
        } 

    }*/

    private void WallSlide()
    {
        rb.velocity = new Vector2(rb.velocity.x, -f_slideSpeed);
    }







   

}
