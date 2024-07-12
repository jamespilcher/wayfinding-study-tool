using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AverageDistanceFromLine
{
    private static float sumOfDistances = 0f;
    private static int distanceMeasurementsCount = 0;

    // Calculate the minimum distance from a point to a line segment
    public static float DistancePointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {
        Vector3 lineDirection = lineEnd - lineStart;
        float lineLength = lineDirection.magnitude;
        lineDirection.Normalize();
        float projectLength = Mathf.Clamp(Vector3.Dot(point - lineStart, lineDirection), 0f, lineLength);
        Vector3 projectPoint = lineStart + lineDirection * projectLength;
        return Vector3.Distance(point, projectPoint);
    }

    // Calculate the distance from the player to the closest point on a LineRenderer line
    public static float ProcessCurrentDistanceToLine(GameObject player, LineRenderer lineRenderer)
    {
        if (lineRenderer != null && player != null)
        {
            float minDistance = float.MaxValue;
            Vector3 playerPosition = player.transform.position;

            // Iterate through each segment of the LineRenderer
            for (int i = 0; i < lineRenderer.positionCount - 1; i++)
            {
                Vector3 start = lineRenderer.GetPosition(i);
                Vector3 end = lineRenderer.GetPosition(i + 1);

                float distance = DistancePointLine(playerPosition, start, end);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
            // Add to sum and increment count
            sumOfDistances += minDistance;
            distanceMeasurementsCount++;
            return minDistance;
        }
        return -1; // Return -1 if either player or lineRenderer is null
    }
    public static float Dump()
{
    if (distanceMeasurementsCount > 0)
    {
        float averageDistance = sumOfDistances / distanceMeasurementsCount;
        // Reset for next set of measurements
        // sumOfDistances = 0f;
        // distanceMeasurementsCount = 0;
        return averageDistance;
    }
    Debug.LogWarning("No measurements were made.");
    return -1; // Return -1 if no measurements were made
}
}