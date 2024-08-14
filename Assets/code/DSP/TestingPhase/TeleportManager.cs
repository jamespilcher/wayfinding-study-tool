using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public GameObject landmarks;
    public GameObject gridTracker;
    public GameObject player;
    public LandmarkPositionsData landmarkPositionsData;
    public DSPPathsData DSPPathsData;

    private int currentPathIndex = 0;
    private Vector2Int startingCoords;
    private string targetLandmark;

    void Start()
    {
        if (DSPPathsData != null && DSPPathsData.DSPPaths.Count > 0)
        {
            // Initialize the starting position and target position
            currentPathIndex = 0;
            startingCoords = DSPPathsData.DSPPaths[currentPathIndex].spawnPosition;
            targetLandmark = DSPPathsData.DSPPaths[currentPathIndex].targetLandmarkName;
            TeleportPlayer(startingCoords);
        }
        else
        {
            Debug.LogError("DSPPathsData is null or contains no paths.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            GameObject targetTrigger = LookUpLandmarkTrigger(targetLandmark);
            if (isPosInTrigger(player.transform.position, targetTrigger))
            {
                dumpLogs();
                // Player has reached the target position, move to the next path
                currentPathIndex++;
                if (currentPathIndex < DSPPathsData.DSPPaths.Count)
                {
                    startingCoords = DSPPathsData.DSPPaths[currentPathIndex].spawnPosition;
                    targetLandmark = DSPPathsData.DSPPaths[currentPathIndex].targetLandmarkName;
                    TeleportPlayer(startingCoords);
                }
                else
                {
                    Debug.Log("Player has completed the study.");
                }
            }
        }
    }

    void dumpLogs()
    {
        Debug.Log("Player has reached the landmark.");
    }

    bool isPosInTrigger(Vector3 pos, GameObject trigger)
    {
        Collider triggerCollider = trigger.GetComponent<Collider>();
        if (triggerCollider != null)
        {
            return triggerCollider.bounds.Contains(pos);
        }
        return false;
    }
    

    public void TeleportPlayer(Vector2Int targetCoords) // fix!
    {
        player.transform.position = GetTriggerFromCoords(targetCoords).transform.position;
    }

public GameObject GetTriggerFromCoords(Vector2Int coords)
{
    // Find the row (y-coordinate)
    Transform rowTransform = gridTracker.transform.Find("y" + coords.y);
    if (rowTransform == null)
    {
        Debug.LogError("Row y" + coords.y + " not found.");
        return null;
    }

    // Find the column (x-coordinate)
    Transform columnTransform = rowTransform.Find("x" + coords.x);
    if (columnTransform == null)
    {
        Debug.LogError("Column x" + coords.x + " not found.");
        return null;
    }

    // Return the found GameObject
    return columnTransform.gameObject;
}

    public GameObject LookUpLandmarkTrigger(string landmarkName)
    {
        foreach (var landmark in landmarkPositionsData.landmarkPositions)
        {
            if (landmark.landmarkName == landmarkName)
            {
                return GetTriggerFromCoords(landmark.position);
            }
        }
        Debug.LogError("Landmark not found: " + landmarkName);
        return null;
    }
}