/**
* Universidad de La Laguna"
* Project:  Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: StatisticsManager.cs: This file contains the class used to allocate and 
*       manage of all the challenges solutions statistics information.
*/

/// <summary>
/// This class is used to allocate and manage the statistics information of all 
/// the solved challenges.

/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : MonoBehaviour
{
    public static StatisticsManager statisticsManagerInstance;

    // This dictionary stores all the challenges that have been completed and their respective solutions.
    private Dictionary<string, List<ChallengeSolution>> challengesSolutions = new Dictionary<string, List<ChallengeSolution>>();

    private int usedBlocks = 0;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (!Finder.CheckContains("StatisticsManager"))
        {
            DontDestroyOnLoad(this.gameObject);
            Finder.AddObject(this.gameObject);
        }
    }

    /// <summary>
    /// Increase the number of blocks by one unit
    /// </summary>
    public void IncreaseUsedBlocks()
    {
        usedBlocks++;
    }

    /// <summary>
    /// Decrease the number of blocks by one unit
    /// </summary>
    public void DecreaseUsedBlocks()
    {
        if (usedBlocks > 0)
        {
            usedBlocks--;
        }
    }

    /// <summary>
    /// Reset the number of used blocks
    /// </summary>
    public void CleanUsedBlocks()
    {
        usedBlocks = 0;
    }

    public int GetUsedBlocks()
    {
        return usedBlocks;
    }

    
    public Dictionary<string, List<ChallengeSolution>> GetChallengesSolutions()
    {
        return challengesSolutions;
    }

    /// <summary>
    /// Adds a new solution for a specific challenge if it exists as a key of
    /// "challengesSolutions" or adds it as new element to the dictionary.
    /// </summary>
    /// <param name="challengeName"> Is the challenge name used as key to look for in the dictionary. </param>
    /// <param name="solution"> Is the new solution to add to a specific challenge (key). </param>
    public void AddNewChallengeSolution(string challengeName, ChallengeSolution solution)
    {
        if ((challengesSolutions != null) && (challengesSolutions.ContainsKey(challengeName)))
        {
            challengesSolutions[challengeName].Add(solution);
        } else {
            List<ChallengeSolution> newListToAdd = new List<ChallengeSolution>();
            newListToAdd.Add(solution);
            challengesSolutions.Add(challengeName, newListToAdd);
        }
    }

    /// <summary>
    /// Looks for the best time and best blocks number solution. It is done in the same function to save doing the same loop twice.
    /// </summary>
    public void SetBestTimeBlocksSoloution()
    {
        foreach (var currentSolution in challengesSolutions)
        {
            int minTimeIndex = 0;
            int minBlocksIndex = 0;

            currentSolution.Value[minBlocksIndex].SetBestBlocks(true);
            currentSolution.Value[minTimeIndex].SetBestTime(true);
            if (currentSolution.Value.Count > 1)
            {
                for (int i = 0; i < currentSolution.Value.Count; i++)
                {
                    if (currentSolution.Value[i].GetBlocksNumber() <    
                        currentSolution.Value[minBlocksIndex].GetBlocksNumber())
                        
                    {
                        currentSolution.Value[minBlocksIndex].SetBestBlocks(false);
                        currentSolution.Value[i].SetBestBlocks(true);
                        minBlocksIndex = i;
                    }
                    if (currentSolution.Value[i].GetSolutionTimeFloat()[0] <    
                        currentSolution.Value[minTimeIndex].GetSolutionTimeFloat()[0])
                    {
                        currentSolution.Value[minTimeIndex].SetBestTime(false);
                        currentSolution.Value[i].SetBestTime(true);
                        minTimeIndex = i;
                    } else if((currentSolution.Value[i].GetSolutionTimeFloat() 
                              [0] == currentSolution.Value[minTimeIndex].GetSolutionTimeFloat()[0]) && 
                              (currentSolution.Value[i].GetSolutionTimeFloat()
                               [1] < currentSolution.Value[minTimeIndex].GetSolutionTimeFloat()[1]))
                    {
                        currentSolution.Value[minTimeIndex].SetBestTime(false);
                        currentSolution.Value[i].SetBestTime(true);
                        minTimeIndex = i;
                    }
                }
            }
        }
    }
}
