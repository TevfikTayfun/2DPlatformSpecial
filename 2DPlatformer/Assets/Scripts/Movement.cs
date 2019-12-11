using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private PlayerCollisions coll;

    private int direction;

    [Header("Floats")]
    [SerializeField]
    private float f_speed = 10f;
    [SerializeField][Range(1,10)]
    private float f_jumpForce;
    [SerializeField]
    private float f_slideSpeed = 2.2f;
    [Space]
    public float f_fallMultiplier = 2.5f;
    public float f_lowJumpMultiplier = 2f;
    public float wallJumpLerp = 10f;

    private float f_dashSpeed = 20f;
    private float f_dashTime;
    private float f_startDashTime = 0.1f;

    private Rigidbody2D rb;

    [Header("Bools")]
    private bool wallGrab;
    private bool canMove;
    private bool wallJumped;
    private bool isDashing;

    [Space]
    private bool hasDashed;



    private float f_buttonCooler = 0.5f ; // Half a second before reset
    private float f_buttonCount = 0;


    void Start()
    {

        f_dashTime = f_startDashTime;
        coll = GetComponent<PlayerCollisions>();
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(x, y);

        Walk(dir);

        DirectionalDash();

        wallGrab = coll.onWall && Input.GetKey(KeyCode.LeftShift);

        if (wallGrab)
        {
            rb.velocity = new Vector2(rb.velocity.x, y * f_speed);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(coll.onWall && !coll.onGround && !Input.GetKey(KeyCode.LeftShift))
        {
            WallSlide();
        }


    }


    private void Walk(Vector2 dir)
    {

        //if (!wallJumped)
            rb.velocity = (new Vector2(dir.x * f_speed, rb.velocity.y));
        /*else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * f_speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }*/
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && (coll.onGround))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * f_jumpForce;
        }

        /*if(wallGrab && coll.onLeftWall && Input.GetButton("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * f_jumpForce;
        }

        if (coll.onRightWall && Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.left * f_jumpForce;
        }*/
    }

    private void WallSlide()
    {
        rb.velocity = new Vector2(rb.velocity.x, -f_slideSpeed);
    }


    /*private void Dash(float x, float y)
    {
        rb.velocity = Vector2.zero;
        rb.velocity += new Vector2(x, y).normalized * 125;
    }*/

    private void DirectionalDash()
    {

        if (direction == 0)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                if (f_buttonCooler > 0 && f_buttonCount == 1/*Number of Taps you want Minus One*/)
                {
                    direction = 1;
                }
                else
                {
                    f_buttonCooler = 0.5f;
                    f_buttonCount += 1;
                }
            }         
            
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (f_buttonCooler > 0 && f_buttonCount == 1/*Number of Taps you want Minus One*/)
                {
                    direction = 2;
                }
                else
                {
                    f_buttonCooler = 0.5f;
                    f_buttonCount += 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                if (f_buttonCooler > 0 && f_buttonCount == 1/*Number of Taps you want Minus One*/)
                {
                    direction = 3;
                }
                else
                {
                    f_buttonCooler = 0.5f;
                    f_buttonCount += 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (f_buttonCooler > 0 && f_buttonCount == 1/*Number of Taps you want Minus One*/)
                {
                    direction = 4;
                }
                else
                {
                    f_buttonCooler = 0.5f;
                    f_buttonCount += 1;
                }
            }
            if (f_buttonCooler > 0)
            {

                f_buttonCooler -= 1 * Time.deltaTime;

            }
            else
            {
                f_buttonCount = 0;
            }
        }
    
        else
        {
            if(f_dashTime <= 0)
            {
                direction = 0;
                f_dashTime = f_startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                f_dashTime -= Time.deltaTime;
                if(direction == 1)
                {
                    rb.velocity = Vector2.left * f_dashSpeed;
                }
                else if(direction == 2)
                {
                    rb.velocity = Vector2.right * f_dashSpeed;
                }
                else if (direction == 3)
                {
                    rb.velocity = Vector2.up * f_dashSpeed;
                }
                else if (direction == 4)
                {
                    rb.velocity = Vector2.down * f_dashSpeed;
                }
            }
        }
    }


}
