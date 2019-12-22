using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBetweenPoints : MonoBehaviour
{
    Vector3 v_Distance;
    int i_distenceCounter = 0;
    bool b_findDistanceControl = true;
    bool b_IsReachedThePoint = false;

    GameObject[] points;
    [SerializeField] private GameObject g_PatrolPoints;

    void Start()
    {
        // Get the points that the enemy patrolling between
        points = new GameObject[g_PatrolPoints.transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = g_PatrolPoints.transform.GetChild(0).gameObject;
            points[i].transform.SetParent(transform.parent);
        }
    }

    public void Patrolling(float speed)
    {
        if (b_findDistanceControl)
        {
            v_Distance = (points[i_distenceCounter].transform.position - transform.position).normalized;
            b_findDistanceControl = false;
        }
        float dis = Vector3.Distance(transform.position, points[i_distenceCounter].transform.position);

        transform.position += v_Distance * Time.deltaTime * speed;

        if (dis < 0.5f)
        {
            b_findDistanceControl = true;
            if (i_distenceCounter == points.Length - 1)
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
