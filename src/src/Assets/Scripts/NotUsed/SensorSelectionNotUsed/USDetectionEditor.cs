using System.Collections;
using UnityEngine;
using UnityEditor;

/*[CustomEditor (typeof (USDetection))]
public class USDetectionEditor : Editor
{
    void OnSceneGUI()
    {
        USDetection usDetection = (USDetection)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(usDetection.transform.position, Vector3.up, Vector3.forward, 360, usDetection.soundRadius);
        Vector3 soundAngleA = usDetection.DirectionFromAngle(-usDetection.soundAngle / 2, false);
        Vector3 soundAngleB = usDetection.DirectionFromAngle(usDetection.soundAngle / 2, false);

        Handles.DrawLine(usDetection.transform.position, usDetection.transform.position + soundAngleA * usDetection.soundRadius);
        Handles.DrawLine(usDetection.transform.position, usDetection.transform.position + soundAngleB * usDetection.soundRadius);

        Handles.color = Color.red;
        foreach (Transform detectableObject in usDetection.DetectableObjects)
        {
            Handles.DrawLine(usDetection.transform.position, detectableObject.position);
        }
    }
}*/
