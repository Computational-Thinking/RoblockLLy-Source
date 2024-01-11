using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCodingPanelSize : MonoBehaviour
{
    private Transform codingPanel;
    private const float INCREMENT_FACTOR = 0.11f;
    private const float MAX_INCREMENT = 1.33f;
    private const float MIN_INCREMENT = 0.66f;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        codingPanel = GameObject.Find("Workspace/CodingPanel").GetComponent<Transform>();
    }

    public void MaximizeCodingPanel()
    {
        if(codingPanel.localScale.x < MAX_INCREMENT){
            codingPanel.localScale += new Vector3(INCREMENT_FACTOR, INCREMENT_FACTOR, 0);
        }
    }

    public void MinimizeCodingPanel()
    {
        if(codingPanel.localScale.x > MIN_INCREMENT){
            codingPanel.localScale -= new Vector3(INCREMENT_FACTOR, INCREMENT_FACTOR, 0);
        } 
    }
}
