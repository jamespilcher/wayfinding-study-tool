using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataCollector
{
    private static List<Dictionary<string, string>> trialsData = new List<Dictionary<string, string>>();
    private static Dictionary<string, string> currentTrialData;

    public static void StartTrackingTrialData(GameObject player) // trial number, 
    {
        TimeTaken.StartTimer();
        GranularDistanceTravelled.StartPosition(player.transform.position);
        ManhattanPathing.StartCoords(player.transform.position);
        // get trigger the player is standing in
        
        currentTrialData = new Dictionary<string, string>();
        Debug.Log("Start Tracking Trial Data.");
    }

    public static void UpdateTrackingTrialData(GameObject player)
    {
        GranularDistanceTravelled.ProcessDistanceToNewPosition(player.transform.position);
        ManhattanPathing.UpdateCoords(player.transform.position);
    }

    public static void FinishTrackingTrialData(int trialNumber, Vector2Int spawnPosition, string targetLandmark)
    {
        if (currentTrialData != null)
        {
            currentTrialData["participantID"] = StudyConfig.Instance.participantID.ToString(); // TODO
            currentTrialData["trialNumber"] = trialNumber.ToString();
            currentTrialData["spawnPosition"] = spawnPosition.ToString().Replace(",", "");
            currentTrialData["targetLandmark"] = targetLandmark;
            currentTrialData["timeTaken"] = TimeTaken.Dump().ToString();
            currentTrialData["granularDistanceTravelled"] = GranularDistanceTravelled.Dump().ToString();
            currentTrialData["ManhattanRoute"] = ManhattanPathing.DumpMannhattanRouteAsString();
            currentTrialData["ManhattanRouteDistance"] = ManhattanPathing.DumpDistanceOfLine().ToString();
            trialsData.Add(currentTrialData);
            currentTrialData = null;
        }
        Debug.Log("Data saved to memory");
    }

    public static void SaveDataToFile()
    {
        string filePath = "Assets/Code/DSP/TestingPhase/trialData" + StudyConfig.Instance.studyName + ".csv";
        bool fileExists = File.Exists(filePath);

        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            if (trialsData.Count > 0)
            {
                if (!fileExists && trialsData.Count > 0)
                {
                    var header = string.Join(",", trialsData[0].Keys);
                    writer.WriteLine(header);
                }

                // Write the data
                foreach (var trial in trialsData)
                {
                    var line = string.Join(",", trial.Values);
                    writer.WriteLine(line);
                }
            }

        }

        Debug.Log("Data saved to file.");
    }
}