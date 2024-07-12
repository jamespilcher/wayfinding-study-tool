using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlanner : MonoBehaviour
{
    // Start is called before the first frame update
    // empty object 'landmarks' exists in scene, has children that are landmarks
    public GameObject landmarks;
    public GameObject player;

    private GameObject selectedLandmark;

    private bool completed = false;
    private bool statsDumped = false;

    void Start()
    {
        if (landmarks.transform.childCount > 0)
            {
                int index = Random.Range(0, landmarks.transform.childCount);
                selectedLandmark = landmarks.transform.GetChild(index).gameObject;
                Debug.Log("Selected landmark: " + selectedLandmark.name);

                Stats.landmark = selectedLandmark; // Set the static property
                Stats.player = player; // Set the static property
                Stats.StartTracking();
                
                completed = false;
            }
        else
            {
                Debug.LogWarning("Landmarks object has no children.");
            }
    }

        // Update is called once per frame
    void Update()
    {
        if (Stats.landmark == null  || player == null){
            Debug.LogError("Landmark or player object is missing.");
            return;
        }
        // Has the player reached the landmark?
        float distance = Vector3.Distance(player.transform.position, selectedLandmark.transform.position);

        if (distance < 10.0f) // Assuming 1.0f as the threshold for reaching the landmark
        {
            completed = true;
        }

        if (!completed){
            Stats.UpdateStats();
        }
        else {
            if (!statsDumped){
                Stats.DumpStats();
                statsDumped = true;
            }
        }
    }
}
