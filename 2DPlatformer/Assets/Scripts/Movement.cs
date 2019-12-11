using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    [SerializeField]
    private float f_speed = 10f;
    [SerializeField][Range(1,10)]
    private float f_jumpForce;

    public float _fallMultiplier = 2.5f;
    public float _lowJumpMultiplier = 2f;
    
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Walk(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * f_speed, rb.velocity.y));
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * f_jumpForce;
        }
    }
}
