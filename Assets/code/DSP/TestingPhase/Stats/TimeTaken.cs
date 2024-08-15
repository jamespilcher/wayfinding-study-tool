using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeTaken
{
    // Start is called before the first frame update
    private static float startTime = 0f;
    private static bool timerStarted = false;

    public static void StartTimer(){
        startTime = Time.time;
        timerStarted = true;
    }

    public static float Dump(){
        if (!timerStarted){
            Debug.LogError("Timer was not started.");
            return -1;
        }
        return Time.time - startTime;
    }
}
