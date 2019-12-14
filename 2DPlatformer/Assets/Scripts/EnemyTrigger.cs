using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    EnemyController enemyController;

    private void Start()
    {
        enemyController = transform.parent.gameObject.GetComponent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyController.b_IsPlayerSeenByEnemy = true;
            Debug.Log("true");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyController.b_IsPlayerSeenByEnemy = false;
        }
    }
}
