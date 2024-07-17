using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameConfig
{
    public bool hasTimer = false;
    public bool hasDirectionSignposts = false;
    public bool hasLocationSignposts = false;
    public float timeLimit = 0f;
    // Add other configuration settings as needed
}