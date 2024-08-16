using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LandmarkPosition
{
    public string landmarkName;
    public Vector2Int position;

    public LandmarkPosition(string landmarkName, Vector2Int position)
    {
        this.landmarkName = landmarkName;
        this.position = position;
    }
}


[CreateAssetMenu(fileName = "LandmarkPositionsSO", menuName = "ScriptableObjects/LandmarkPositionsData", order = 1)]
public class LandmarkPositionsData : ScriptableObject
{
    public List<LandmarkPosition> landmarkPositions = new List<LandmarkPosition>();
} 
