using System.Collections.Generic;
using UnityEngine;

public class LandmarkInfo : MonoBehaviour
{
    [SerializeField]
    private LandmarkPositionsData landmarkPositionsData;

    public GameObject gridTracker;
    public GameObject landmarks;

    void Start()
    {
        InitializeLandmarkPositions();
    }

    public void InitializeLandmarkPositions()
    {
        Debug.Log("Initializing landmark positions...");
        landmarkPositionsData.landmarkPositions.Clear();

        if (landmarks != null && gridTracker != null)
        {
            foreach (Transform landmark in landmarks.transform)
            {
                Collider[] allTriggers = gridTracker.GetComponentsInChildren<Collider>();
                foreach (Collider trigger in allTriggers)
                {
                    if (trigger.isTrigger && trigger.bounds.Contains(landmark.position))
                    {
                        Vector2Int triggerPosition = GetTriggerPosition(trigger.gameObject);
                        landmarkPositionsData.landmarkPositions.Add(new LandmarkPosition(landmark.name, triggerPosition));
                    }
                }
            }
        }
    }

    Vector2Int GetTriggerPosition(GameObject trigger)
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