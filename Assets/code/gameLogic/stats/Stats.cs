using UnityEngine;

public static class Stats
{
    public static GameObject player;
    public static GameObject landmark; // The current target landmark

    public static void StartTracking()
    {
        TimeTaken.StartTimer();
        DistanceTravelled.StartPosition(player.transform.position);
        DistanceOfLine.ProcessDistanceOfLine(landmark.GetComponentInChildren<LineRenderer>());
    }

    // Static method to be called from GamePlanner's Update loop
    public static void UpdateStats()
    {
      AverageDistanceFromLine.ProcessCurrentDistanceToLine(player, landmark.GetComponentInChildren<LineRenderer>());
      DistanceTravelled.ProcessDistanceToNewPosition(player.transform.position);
    }

    public static void DumpStats()
    {
        Debug.Log(DistanceOfLine.Dump()); //todo, fix line height, make it flat on the ground
        Debug.Log(TimeTaken.Dump());
        Debug.Log(AverageDistanceFromLine.Dump());
        Debug.Log(DistanceTravelled.Dump());
        // PATH TAKEN
        Debug.Log("Player has reached the landmark.");
    }
}