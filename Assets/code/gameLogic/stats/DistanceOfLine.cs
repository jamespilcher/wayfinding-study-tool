using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DistanceOfLine
{
    private static float distanceOfLine = 0f;
   public static void ProcessDistanceOfLine(LineRenderer lineRenderer){
         float totalDistance = 0f;
         if (lineRenderer != null)
         {
              for (int i = 0; i < lineRenderer.positionCount - 1; i++)
              {
                totalDistance += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
              }
              distanceOfLine = totalDistance;
         }
         else{
            Debug.LogWarning("No lineRenderer was provided.");

         }
   }
   public static float Dump(){
       return distanceOfLine;
   }
}