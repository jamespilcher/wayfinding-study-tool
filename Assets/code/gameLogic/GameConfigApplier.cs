using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigApplier
{
    // Define a map from configuration flags to their handling actions
    private static Dictionary<string, Action<bool>> configHandlers = new Dictionary<string, Action<bool>>();

    static GameConfigApplier()
    {
        // Initialize the map with handlers for each configuration option
        configHandlers.Add("timerEnabled", HandleTimerEnabled);
        // Add other configuration handlers here
    }

    public static void ApplyGameConfig(GameConfig gameConfig)
    {
        // Apply each configuration by invoking the corresponding handler from the map
        foreach (var handler in configHandlers)
        {
            // Use reflection to get the value of the configuration flag by name
            var property = typeof(GameConfig).GetField(handler.Key);
            if (property != null)
            {
                bool value = (bool)property.GetValue(gameConfig);
                handler.Value.Invoke(value);
            }
        }

        Debug.Log("Applying game config...");
    }

    // Handler for the "timerEnabled" configuration
    private static void HandleTimerEnabled(bool isEnabled)
    {
        if (isEnabled)
        {
            Debug.Log("Timer enabled");
        }
        else
        {
            Debug.Log("Timer disabled");
        }
    }

    // Add other handlers for different configuration options here
}