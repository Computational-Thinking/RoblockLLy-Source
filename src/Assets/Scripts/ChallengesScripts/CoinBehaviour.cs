/**
* Universidad de La Laguna"
* Project:  Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 27/07/2021
* File: CoinRotation.cs: This file contains the class used to manage 
*       the behaviour of the coin located at the end of the challenges.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the coin rotation and the collection of statistical data 
/// when the robot collides with the coin.
/// </summary>
public class CoinBehaviour : MonoBehaviour
{
    // This delegathe is used to stop timer from the TimerBehaviour class.
    public delegate void SetTimerOff();
    public static SetTimerOff StopTimer;
    public float rotationSpeed = 60.0f;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        RotateCoin();
    }

    /// <summary>
    /// Performs the rotation of the coin every frame.
    /// </summary>
    private void RotateCoin()
    {
        gameObject.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.
    /// </param>
    void OnTriggerEnter(Collider other)
    {
        SetFinishInformation(other);
    }

    /// <summary>
    /// This method is called when the robot collides with the coin.
    /// </summary>
    /// <param name="other">The other object is the robot collider involved in 
    /// the collision
    /// </param>
    private void SetFinishInformation(Collider other)
    {
        if (gameObject.activeSelf)
        {
            Debug.Log("Ha entrado en el trigger enter");
            if (other.gameObject.tag == "SelectedRobot" ||
                other.gameObject.name == "Contact")
            {
                GameObject selectedRobot = GameObject.FindWithTag("SelectedRobot").gameObject;
                selectedRobot.GetComponent<RobotMotionController>().StopRobotNow();
                gameObject.SetActive(false);
                ChallengeSolution newSolution = new ChallengeSolution();
                // Add time elapsed to complet the challenge:
                newSolution.SetSolutionTime(GameObject.FindWithTag("Timer").GetComponent<TimerBehaviour>().GetTimeString());
                // Add time elapsed to complet the challenge in string format:
                newSolution.SetSolutionTimeFloat(GameObject.FindWithTag("Timer").GetComponent<TimerBehaviour>().TimerFunction());
                // Set the number of used blocks to complete this solution:
                newSolution.SetBlocksNumber(GameObject.FindWithTag("StatisticsManager").GetComponent<StatisticsManager>().GetUsedBlocks());
                // Set of the progress in this solution:
                string challengeKey = CalculateChallengeKey();
                newSolution.CalculateProgress(challengeKey);
                GameObject.FindWithTag("StatisticsManager").GetComponent<StatisticsManager>().AddNewChallengeSolution(challengeKey, newSolution);
                StopTimer();
            }
        }
    }

    /// <summary>
    /// Is used to calculate the key from the current scene name.
    /// </summary>
    /// <return> The challange key </returns>
    private string CalculateChallengeKey()
    {
        string challengeKey;
        switch (SceneManager.GetActiveScene().name)
        {
            case "LabyrinthChallenge":
                challengeKey = "Labyrinth";
                break;
            case "IRChallenge":
                challengeKey = "IR";
                break;
            case "ColorChallenge":
                challengeKey = "Color";
                break;
            case "BalanceChallenge":
                challengeKey = "Gyroscope";
                break;
            default:
                challengeKey = "NotChallengeDetected";
                break;
        }
        return challengeKey;
    }
}
