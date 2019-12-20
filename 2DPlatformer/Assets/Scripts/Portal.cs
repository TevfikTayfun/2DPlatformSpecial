using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination;
    float distance = 0.3f;
    public bool isPortal1;

    float cooldown = 1;
    float cooldownTimer;

    private void Start()
    {
         if(isPortal1 == false)
         {
            destination = GameObject.FindGameObjectWithTag("Portal1").GetComponent<Transform>();
         }

         else
         {   
           destination = GameObject.FindGameObjectWithTag("Portal2").GetComponent<Transform>();
         }
    }

    private void Update()
    {
        if(cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0)
            cooldownTimer = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Vector2.Distance(transform.position, collision.transform.position) > distance && cooldownTimer <= 0 )
        {
            collision.transform.position = new Vector2(destination.position.x , destination.position.y);
            cooldownTimer = cooldown;
            
        }
    }
}
