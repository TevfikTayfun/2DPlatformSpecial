using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PatrolGizmos : MonoBehaviour
{

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            // Create red spheres
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            // Yellow lines between spheres
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
    }
#endif

}

// EDITOR CODES

#if UNITY_EDITOR
[CustomEditor(typeof(PatrolGizmos))]
[System.Serializable]

class enemyPatrolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PatrolGizmos script = (PatrolGizmos)target; // Reach the codes above
        if (GUILayout.Button("CREAT POINT", GUILayout.MinWidth(100), GUILayout.MinHeight(20)))
        {
            GameObject newPosition = new GameObject(); // Create new object
            newPosition.transform.parent = script.transform; // Define parent as this enemy
            newPosition.transform.position = script.transform.position;
            newPosition.name = script.transform.childCount.ToString(); // Define name as a child count
        }
    }
}
#endif   
