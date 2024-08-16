using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class TrialManager : MonoBehaviour
{
    public StudyConfig studyConfig = StudyConfig.Instance;
    public DSPPathsData DSPPathsData;
    public LandmarkPositionsData landmarkPositionsData;
    public GameObject landmarks;
    public GameObject gridTracker;
    public GameObject player;
    public TMP_Text trialTargetText;


    private List<DSPPath> DSPPaths;
    private int currentPathIndex = 0;
    private Vector2Int startingCoords;
    private string targetLandmark;

    private bool participantFinished = false;

    void Start()
    {

        
        if (DSPPathsData != null && DSPPathsData.DSPPaths.Count > 0)
        {
            DSPPaths = DSPPathsData.DSPPaths;
            if (studyConfig.shuffleDSPPaths)
            {
                ShuffleDSPPaths();
            }
            // Initialize the starting position and target position
            currentPathIndex = 0;
            // start tracking data
            RunTrial(currentPathIndex);
        }
        else
        {
            Debug.LogError("DSPPathsData is null or contains no paths.");
        }
    }

    void Update()
    {
        if (player != null && !participantFinished)
        {
            GameObject targetTrigger = LookUpLandmarkTrigger(targetLandmark);
            DataCollector.UpdateTrackingTrialData(player);
            if (isPosInTrigger(player.transform.position, targetTrigger))
            {
                DataCollector.FinishTrackingTrialData(currentPathIndex, startingCoords, targetLandmark);
                currentPathIndex++;
                if (currentPathIndex < DSPPaths.Count)
                {
                    RunTrial(currentPathIndex);
                }
                else
                {
                    ParticipantCompleted();
                }
            }
        }
    }

    void ShuffleDSPPaths()
    {
        // fisher-yates shuffle
        System.Random rng = new System.Random();
        int n = DSPPaths.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            DSPPath value = DSPPaths[k];
            DSPPaths[k] = DSPPaths[n];
            DSPPaths[n] = value;
        }
    }

    void ParticipantCompleted()
    {
        participantFinished = true;
        Debug.Log("Player has completed the study.");
        DataCollector.SaveDataToFile(); // todo: fix path
    }

    void DisplayTargetLandmark()
    {
        // trialTargetText.text = "Find " + targetLandmark; // beautify the text!!!


        string beautifiedTargetLandmark = Regex.Replace(targetLandmark, "(\\B[A-Z])", " $1");
        trialTargetText.text = "Find " + beautifiedTargetLandmark;


        Debug.Log("Next trial: " + targetLandmark);
    }


    void RunTrial(int n){
        startingCoords = DSPPaths[currentPathIndex].spawnPosition;
        targetLandmark = DSPPaths[currentPathIndex].targetLandmarkName;
        TeleportPlayerToCoords(startingCoords);
        DisplayTargetLandmark();
        DataCollector.StartTrackingTrialData(player);
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
    

    public void TeleportPlayerToCoords(Vector2Int targetCoords) // fix!
    {
        Debug.Log("Teleporting player to " + targetCoords);
        Vector3 targetPosition = GetTriggerFromCoords(targetCoords).transform.position;
        player.transform.position = new Vector3(targetPosition.x, player.transform.position.y, targetPosition.z);
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