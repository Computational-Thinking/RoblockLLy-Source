/**
* Universidad de La Laguna
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 18/06/2021
* File: CheckMicrophone.cs : This file contains the logic for the Microphone 
*       checkbox selection.
*/



// Unused script




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMicrophone : MonoBehaviour
{
    public delegate void MicrophoneChecked();
    public static MicrophoneChecked CheckboxMicrophone;
    public void Acivation()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            //CheckboxMicrophone();   
        } else {
            gameObject.SetActive(false);
        }
    }
}
