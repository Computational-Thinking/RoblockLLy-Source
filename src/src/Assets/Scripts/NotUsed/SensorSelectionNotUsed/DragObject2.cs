using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject2 : MonoBehaviour
{
    public delegate void DraggEnded(DragObject2 sensorToDrag);
    public DraggEnded DragFinished;
    //private Vector3 MouseOffset; 
    private float MouseZCoord; // Stores the Z coordinate of the mouse in a screen point.

    private bool dragged = false; // True if the sensor is dragged.

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    /*void OnMouseDown()
    {
        MouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        MouseOffset = gameObject.transform.position - GetMouseWorldPos();
    }*/

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!Input.GetMouseButton(0) && dragged) // If drag ends.
        {
            dragged = false;
            DragFinished(this); // Call delegate for snap the sensor.
        } else if (dragged)
        {
            transform.position = GetMouseWorldPos();
        }
    }

    /// <summary>
    /// ManageDrag is called when the sensor is instantiated in the 
    /// "UltrasoundBox script" to allow the spawn and drag of the sensor.
    /// </summary>
    public void ManageDrag()
     {
        // Takes the Z coordinate of the sensor position and convert it to screen point.
        MouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        //MouseOffset = gameObject.transform.position - GetMouseWorldPos();
        transform.position = GetMouseWorldPos(); // Move the object to the same 
                                                 // position of the mouse.
        dragged = true;
     }

    /// <summary>
    /// GetMouseWorldPos is used to give the mouse position on World 
    /// representation.
    /// </summary>
    private Vector3 GetMouseWorldPos()
    {
        Vector3 MousePoint = Input.mousePosition;
        MousePoint.z = MouseZCoord;
        return Camera.main.ScreenToWorldPoint(MousePoint);
    }

    /// <summary>
    /// OnMouseDrag is called when the user has clicked on a GUIElement or 
    /// Collider
    /// and is still holding down the mouse.
    /// </summary>
    /*void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos();
    }*/

    /// <summary>
    /// OnMouseUp is called when the user has released the mouse button.
    /// </summary>
    /*void OnMouseUp()
    {
        Debug.Log("Mouse up");
        dragged = false;
        DragFinished(this);
    }*/
}
