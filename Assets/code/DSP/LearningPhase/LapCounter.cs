using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LapCounter : MonoBehaviour
{
    public TMP_Text lapCounterText; // Reference to the UI Text component
    public GameObject OneWayWall;

    private int lapCount = 0; // Variable to store the lap count
    private bool isWallEnabled = true; // Flag to track the state of the wall

    void SetWallState(bool enable)
    {
        isWallEnabled = enable;
        OneWayWall.GetComponent<Collider>().enabled = enable;
    }

    void Start()
    {
        // Initialize the lap counter text
        UpdateLapCounterText();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger box
        if (other.CompareTag("Player"))
        {
            // Allow the player to pass through
            SetWallState(false);

            Debug.Log("Player detected entering from the west");
            lapCount++; // Increment the lap count
            UpdateLapCounterText(); // Update the UI text
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Re-enable collision when the player exits the trigger
        if (other.CompareTag("Player"))
        {
            SetWallState(true);
        }
    }

    void UpdateLapCounterText()
    {
        // Check if lapCounterText is assigned
        if (lapCounterText == null)
        {
            Debug.LogError("lapCounterText is not assigned in the Inspector.");
            return;
        }

        // Update the UI text with the current lap count
        lapCounterText.text = "Laps: " + lapCount;
    }
}