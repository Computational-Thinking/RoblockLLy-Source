using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USDetectionAux : MonoBehaviour
{
    public float soundRadius;
    [Range(0,360)]
    public float soundAngle;
    public LayerMask detectableObject;
    public LayerMask notDetectableObject;

    [HideInInspector]
    public List<Transform> DetectableObjects = new List<Transform>();

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        StartCoroutine("FindDetectableObjectsWithDelay", 0.2f);
    }

    IEnumerator FindDetectableObjectsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindDetectableObjects();
        }
    }

    void FindDetectableObjects()
    {
        DetectableObjects.Clear();
        Collider[] detectableObjectsInSoundRadius = Physics.OverlapSphere(transform.position, soundRadius, detectableObject);
        for (int i = 0; i < detectableObjectsInSoundRadius.Length; i++)
        {
            Transform auxDetectableObject = detectableObjectsInSoundRadius[i].transform;
            Vector3 dirToDetectableOnject = (auxDetectableObject.position - transform.position).normalized;
            float aux = Vector3.Angle(transform.forward, dirToDetectableOnject);
            float result = (soundAngle / 2);
            if (aux < result)
            {
                // Check if there is an obstacle in front of the sensor that doesn't have to be detected.
                float distToDetectObject = Vector3.Distance(transform.position, auxDetectableObject.position);
                if (!Physics.Raycast(transform.position, dirToDetectableOnject, distToDetectObject, notDetectableObject)) 
                {
                    DetectableObjects.Add(auxDetectableObject);
                    Debug.DrawLine(transform.position, auxDetectableObject.position, Color.blue, 5.0f);
                }
                //18:03
            }
        }
    }

    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
