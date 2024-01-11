using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAble : MonoBehaviour
{
    private static readonly Dictionary<string, GameObject> findAblesHash = new Dictionary<string, GameObject>();

    public static FindAble FindAbleInstance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (FindAble.FindAbleInstance == null)
        {
            FindAble.FindAbleInstance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //Debug.Log(findAblesHash);
    }

    public static GameObject FindObject(string search)
    {
        if(!findAblesHash.ContainsKey(search)) return null;

        return findAblesHash[search];
    }

    public static void AddObject(string name, GameObject objectToAdd)
    {
        findAblesHash.Add(name, objectToAdd);
    }

    /*private void OnDestroy()
    {
        if(findAblesHash.ContainsKey(name))
        {
            findAblesHash.Remove(name);
        }
    }*/
}
