using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // VARIBLES FOR PATROLLING
    Vector3 v_Distance;
    int i_distenceCounter = 0;
    bool b_findDistanceControl = true;
    bool b_IsReachedThePoint = false;
    GameObject[] temp;
    [SerializeField] private GameObject g_PatrolPoints;

    void Start()
    {
        temp = new GameObject[g_PatrolPoints.transform.childCount];

        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = g_PatrolPoints.transform.GetChild(0).gameObject;
            temp[i].transform.SetParent(transform.parent);
        }
    }

    void FixedUpdate()
    {
        PatrolBetweenPoints();

    }

    // Patrol Between Child Points
    void PatrolBetweenPoints()
    {
        if (b_findDistanceControl)
        {
            v_Distance = (temp[i_distenceCounter].transform.position - transform.position).normalized;
            b_findDistanceControl = false;
        }
        float dis = Vector3.Distance(transform.position, temp[i_distenceCounter].transform.position);

        transform.position += v_Distance * Time.deltaTime * 10; // WE USE RIGIDBODY INSTEAD

        if (dis < 0.5f)
        {
            b_findDistanceControl = true;
            if (i_distenceCounter == temp.Length - 1)
            {
                b_IsReachedThePoint = false;
            }
            else
            if (i_distenceCounter == 0)
            {
                b_IsReachedThePoint = true;
            }

            if (b_IsReachedThePoint)
            {
                i_distenceCounter++;
            }
            else
            {
                i_distenceCounter--;
            }
        }
    }
}
