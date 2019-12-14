using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
	private int i_direction;
	private Rigidbody2D rb;
	public float f_dashSpeed;
	private float f_dashTime;
	public float f_startDashTime;



	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		f_dashTime = f_startDashTime;
	}

	private void Update()
	{
		if(i_direction == 0)
		{
			if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.C))
			{
				i_direction = 1;
			}
			else if(Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.C))
			{
				i_direction = 2;
			}
			else if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.C))
			{
				i_direction = 3;
			}
			else if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.C))
			{
				i_direction = 4;
			}
		}
		else
		{
			if(f_dashTime <= 0)
			{
				i_direction = 0;
				f_dashTime = f_startDashTime;
				//rb.velocity = Vector2.zero;
			}
			else
			{
				f_dashTime -= Time.deltaTime;

				if( i_direction == 1)
				{
					rb.velocity = Vector2.left * f_dashSpeed;
				}
				else if(i_direction == 2)
				{
					rb.AddForce(Vector2.right * f_dashSpeed, ForceMode2D.Impulse);				
				}
				else if (i_direction == 3)
				{
					rb.velocity = Vector2.up * f_dashSpeed;
				}
				else if (i_direction == 4)
				{
					rb.velocity = Vector2.down * f_dashSpeed;
				}
			}
		}
	}

}
