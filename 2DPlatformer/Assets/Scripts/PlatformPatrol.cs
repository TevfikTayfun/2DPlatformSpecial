using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPatrol : MonoBehaviour
{
    PatrolBetweenPoints patrolBetweenPoints;

    void Start()
    {
        patrolBetweenPoints = gameObject.GetComponent<PatrolBetweenPoints>();
    }

    void FixedUpdate()
    {
        patrolBetweenPoints.Patrolling(2);
    }
}
