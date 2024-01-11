/**
* Universidad de La Laguna
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: ErrorPanel.cs : This file contains the 
*       "ErrorPanel" class implementation, which is used to manage the
*       errors console that is displayed when an error occurs.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPanel : MonoBehaviour
{
    public GameObject errorPanel;
        
    /// <summary>
    /// Displays an error on the error panel of the scene.
    /// </summary>
    /// <param name="errorToShow"> The content of the error to show.
    /// </param>
    public void ShowErrorSensorPosition(string errorToShow)
    {
        UBlockly.CSharp.Runner.Stop();
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        errorPanel.transform.GetChild(1).GetComponent<Text>().text = errorToShow;
    }

    /// <summary>
    /// Displays an error on the error panel of the scene.
    /// </summary>
    /// <param name="errorToShow"> The content of the error to show.
    /// </param>
    public void ShowErrorGyroscope()
    {
        UBlockly.CSharp.Runner.Stop();
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        errorPanel.transform.GetChild(1).GetComponent<Text>().text = "El robot no puede tener otro tipo de sensor que no sea el giróscopo";
    }

    /// <summary>
    /// Deactivate the error panel when the user hits the button "Aceptar" on 
    /// the panel
    /// </summary>
    public void CancelPanel()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
