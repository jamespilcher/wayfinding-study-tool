using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DistanceTravelled
{
    // Start is called before the first frame update
    private static float totalDistance = 0f;
    private static Vector3 lastPosition;

    public static void StartPosition(Vector3 position)
    {
        lastPosition = position;
    }

    // Update is called once per frame
    public static void ProcessDistanceToNewPosition(Vector3 newPosition)
    {
        if (lastPosition == newPosition){
            return;
        }
        totalDistance += Vector3.Distance(lastPosition, newPosition);
        lastPosition = newPosition;
    }

    public static float Dump(){
        return totalDistance;
    }

}
