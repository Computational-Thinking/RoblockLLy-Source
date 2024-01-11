using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpTest : MonoBehaviour
{
    public float motorSpeed = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private IEnumerator RotateRobot(float angleToRotate)
    {
        //gameObject.transform.Rotate(degreesToRotate * 20 * Time.deltaTime, 0, 0);
        Quaternion goal = Quaternion.Euler(0, angleToRotate, 0);
        while (Quaternion.Angle(transform.rotation, goal) > 1.0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angleToRotate, 0), Time.deltaTime * motorSpeed);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0,angleToRotate, 0);
        yield return null;
    }

    int aux = 0;
    Quaternion goal = Quaternion.Euler(0, 90, 0);

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime);
    }
}
