/**
* Universidad de La Laguna"
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: ChallegeSolution.cs: This file contains the class used to encapsulate 
*       the information relative to a solution when the user solve a challenge.
*       An object of this class is created when the robot collides with the 
*       coin at the end of each challenge.
*       The information allocated in the objects of this class is used in the
*       StatisticsManager class.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeSolution : MonoBehaviour
{
    private bool bestTime; // Indicates if the solution has been solved in the shortest time.
    private bool bestBlocks; // Indicates if the solution has been solved with the least number of blocks.
    private string solutionTime; // Time elapsed to solve the challenge in string format.
    private List<float> timeFloat; // Time elapsed to solve the challenge in float number format.
    private int blocksNumber; // Number of blocks used to solve the challenge.
    private float progress; // Percentage of proximity to the optimal solution of the challenge.

    private Dictionary<string, int> optimalSolutionsForEachChallenge = new Dictionary<string, int>() {
        {"Labyrinth", 12}, 
        {"IR", 18}, 
        {"Color", 24}, 
        {"Gyroscope", 9}
    };

    public bool GetBestTime()
    {
        return bestTime;
    }

    public void SetBestTime(bool newBestTime)
    {
        bestTime = newBestTime;
    }

    public bool GetBestBlocks()
    {
        return bestBlocks;
    }

    public void SetBestBlocks(bool newBestBlocks)
    {
        bestBlocks = newBestBlocks;
    }

    public string GetSolutionTime()
    {
        return solutionTime;
    }

    public void SetSolutionTime(string newSolutionTime)
    {
        solutionTime = newSolutionTime;
    }

    public List<float> GetSolutionTimeFloat()
    {
        return timeFloat;
    }

    public void SetSolutionTimeFloat(List<float> newSolutionTimeFloat)
    {
        timeFloat = newSolutionTimeFloat;
    }

    public int GetBlocksNumber()
    {
        return blocksNumber;
    }

    public void SetBlocksNumber(int newBlocksNumber)
    {
        blocksNumber = newBlocksNumber;
    }

    public float GetProgress()
    {
        return progress;
    }

    /// <summary>
    /// Method used to calculate the progress of the user to solve a challenge.
    /// </summary>
    /// <param name="challengeKey"> The challenge key to calculate the progress.
    /// </param>
    public void CalculateProgress(string challengeKey)
    {
        int optimalSolution = optimalSolutionsForEachChallenge[challengeKey];
        progress = ((blocksNumber * 100) / optimalSolution);
    }
}
