using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataCollector
{
    private static List<Dictionary<string, string>> trialsData = new List<Dictionary<string, string>>();
    private static Dictionary<string, string> currentTrialData;

    public static void StartTrackingTrialData() // trial number, 
    {
        // Initialize a new dictionary for the current trial
        currentTrialData = new Dictionary<string, string>();
        Debug.Log("Start Tracking Trial Data.");
    }

    public static void UpdateTrackingTrialData()
    {
        // Update the current trial data

        // keys: trial number, time taken, distance travelled, path taken etc
        // values, dumped data from TimeTaken, DistanceTravelled, PathTaken manager classes.

        if (currentTrialData != null)
        {
            currentTrialData["key"] = "value";
        }
        // Debug.Log("Updating stats.");
    }

    public static void FinishTrackingTrialData()
    {
        // Add the current trial data to the list of trials
        if (currentTrialData != null)
        {
            trialsData.Add(currentTrialData);
            currentTrialData = null;
        }
        Debug.Log("Data saved.");
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