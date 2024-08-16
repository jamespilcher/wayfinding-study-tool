using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ManhattanPathing
{
    private static Vector2Int startCoords;
    private static Vector2Int currentCoords;
    private static List<Vector2Int> pathCoords = new List<Vector2Int>();
    private static int distanceOfLine = 0;

    public static void StartCoords(Vector3 position)
    {
        // Do nothing

        startCoords = PositionToCoords(position);
        currentCoords = startCoords;
        pathCoords.Clear();
        distanceOfLine = 0;
        pathCoords.Add(startCoords);
    }

    public static void UpdateCoords(Vector3 newPosition)
    {
        Vector2Int newCoords = PositionToCoords(newPosition);
        if (newCoords != currentCoords)
        {
            pathCoords.Add(newCoords);
            currentCoords = newCoords;
        }
    }

    public static List<Vector2Int> DumpMannhattanRoute(){
        return pathCoords;
    }

    public static string DumpMannhattanRouteAsString(){
        List<string> formattedList = new List<string>();
        foreach (Vector2Int vector in pathCoords)
        {
            formattedList.Add($"({vector.x} {vector.y})");
        }
        return string.Join(" ", formattedList);
    }

    public static int DumpDistanceOfLine(){
        for (int i = 0; i < pathCoords.Count - 1; i++)
        {
            distanceOfLine += Mathf.Abs(pathCoords[i].x - pathCoords[i + 1].x) + Mathf.Abs(pathCoords[i].y - pathCoords[i + 1].y);
        }
        return distanceOfLine;
    }



    public static Vector2Int PositionToCoords(Vector3 position)
    {
        Collider trigger = PositionToTrigger(position);
        Vector2Int coords = GetTriggerPosition(trigger.gameObject);
        return coords;
    }

    public static Collider PositionToTrigger(Vector3 position)
    {
        GameObject gridTracker = GameObject.Find("GridTracker");
        if (gridTracker != null)
        {
            Collider[] allTriggers = gridTracker.GetComponentsInChildren<Collider>();
            foreach (Collider trigger in allTriggers)
            {
                if (trigger.isTrigger && trigger.bounds.Contains(position))
                {
                    return trigger;
                }
            }
        }
        Debug.LogError($"No trigger found for position: {position}");
        return null;
    }


    private static Vector2Int GetTriggerPosition(GameObject trigger)
    {
        Transform parent = trigger.transform.parent;
        if (parent != null && parent.parent != null)
        {
            string yName = parent.name; // e.g., "y1"
            string xName = trigger.name; // e.g., "x1"

            if (yName.Length > 1 && yName[0] == 'y' && int.TryParse(yName.Substring(1), out int y))
            {
                if (xName.Length > 1 && xName[0] == 'x' && int.TryParse(xName.Substring(1), out int x))
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        Debug.LogError($"Failed to parse trigger position for trigger: {trigger.name}");
        return Vector2Int.zero;
    }


}
