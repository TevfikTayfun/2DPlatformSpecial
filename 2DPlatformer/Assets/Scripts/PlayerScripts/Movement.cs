using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private PlayerCollisions coll;
    private PlayerDash dash;
    private int direction;

    [Header("Floats")]
    [SerializeField]
    private float f_speed = 15f;
    [SerializeField][Range(1,10)]
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



    void Start()
    {
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

    }


    private void Walk(Vector2 dir)
    {
      rb.velocity = (new Vector2(dir.x * f_speed, rb.velocity.y));     
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && (coll.onGround) && !coll.onWall)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * f_jumpForce;
        }

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



   

}
