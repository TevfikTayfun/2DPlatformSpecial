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

	public GameObject ghost;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		f_dashTime = f_startDashTime;
	}


	private void Update()
	{
		if(i_direction == 0)
		{
			if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.C))
			{
				i_direction = 1;
			}
			else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.C))
			{
				i_direction = 2;
			}

			else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.C))
			{
				//i_direction = 3;
			}

			else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.C))
			{
				//i_direction = 4;
			}

			else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Space))
			{
				i_direction = 5;
			}

			else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Space))
			{
				i_direction = 6;
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
					GameObject ghostBaby = Instantiate(ghost, transform.position, transform.rotation);
				}

				else if(i_direction == 2)
				{
					rb.velocity = Vector2.right * f_dashSpeed;
					GameObject ghostBaby = Instantiate(ghost, transform.position, transform.rotation);
				}

				else if (i_direction == 3)
				{
					rb.velocity = Vector2.up * f_dashSpeed;
					GameObject ghostBaby = Instantiate(ghost, transform.position, transform.rotation);
				}

				else if (i_direction == 4)
				{
					rb.velocity = Vector2.down * f_dashSpeed;
					GameObject ghostBaby = Instantiate(ghost, transform.position, transform.rotation);
				}

				else if(i_direction == 5)
				{
					rb.velocity = (Vector2.up + Vector2.right).normalized * f_dashSpeed;
					GameObject ghostBaby = Instantiate(ghost, transform.position, transform.rotation);
				}

				else if (i_direction == 6)
				{
					rb.velocity = (Vector2.up + Vector2.left).normalized * f_dashSpeed;
					GameObject ghostBaby = Instantiate(ghost, transform.position, transform.rotation);
				}
			}
		}
	}


}
