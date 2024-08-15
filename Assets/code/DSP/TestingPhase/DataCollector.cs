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
        currentTrialData = new Dictionary<string, string>();
        Debug.Log("Start Tracking Trial Data.");
    }

    public static void UpdateTrackingTrialData(GameObject player)
    {
        GranularDistanceTravelled.ProcessDistanceToNewPosition(player.transform.position);
        // Update the current trial data

        // keys: trial number, time taken, distance travelled, path taken etc
        // values, dumped data from TimeTaken, DistanceTravelled, PathTaken manager classes.


        // Debug.Log("Updating stats.");
    }

    public static void FinishTrackingTrialData(int trialNumber, Vector2Int spawnPosition, string targetLandmark)
    {

        if (currentTrialData != null)
        {
            currentTrialData["participantID"] = "1"; // TODO
            currentTrialData["trialNumber"] = trialNumber.ToString();
            currentTrialData["spawnPosition"] = spawnPosition.ToString().Replace(",", "");
            currentTrialData["targetLandmark"] = targetLandmark;
            currentTrialData["timeTaken"] = TimeTaken.Dump().ToString();
            currentTrialData["granularDistanceTravelled"] = GranularDistanceTravelled.Dump().ToString();
            trialsData.Add(currentTrialData);
            currentTrialData = null;
        }
        Debug.Log("Data saved to memory");
    }

    public static void SaveDataToFile(string filePath)
    {
        // Save data to a csv
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write the header
            if (trialsData.Count > 0)
            {
                var header = string.Join(",", trialsData[0].Keys);
                writer.WriteLine(header);

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