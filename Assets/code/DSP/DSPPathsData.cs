using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DSPPath
{
    public Vector2Int spawnPosition;
    public string targetLandmarkName;

    public DSPPath(Vector2Int spawnPosition, string targetLandmarkName)
    {
        this.spawnPosition = spawnPosition;
        this.targetLandmarkName = targetLandmarkName;
    }
}


[CreateAssetMenu(fileName = "DSPPathsSO", menuName = "ScriptableObjects/DSPPathsData", order = 1)]
public class DSPPathsData : ScriptableObject
{
    public List<DSPPath> DSPPaths = new List<DSPPath>();
}
