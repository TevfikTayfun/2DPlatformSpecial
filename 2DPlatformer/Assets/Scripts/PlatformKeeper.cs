using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformKeeper : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player is on the platform, make player child of platform so that it will follow
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.parent = null;
        }
    }
}
