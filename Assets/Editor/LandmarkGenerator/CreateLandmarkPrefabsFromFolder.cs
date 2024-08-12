using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;


// folder will contain prop prefabs. This script will turn prop prefabs into landmark prefabs by adding the baseLandmark prefab to them, and ensure the line renderer is at the base of the prop.



public static class CreateLandmarkPrefabsFromFolder
{
    public static void ProcessFolder(string propOriginalFolderPath, string baseLandmarkPath, string landmarkPrefabDestinationFolder)
    {
        GameObject baseLandmark = AssetDatabase.LoadAssetAtPath<GameObject>(baseLandmarkPath);
        if (baseLandmark == null)
        {
            Debug.LogError("Base Landmark path is correct.");
            return;
        }

        // Get all prefab files in the folder
        string[] prefabFiles = Directory.GetFiles(propOriginalFolderPath, "*.prefab");

        foreach (string prefabFile in prefabFiles)
        {
            ProcessPrefab(prefabFile, baseLandmark, landmarkPrefabDestinationFolder);
        }

        Debug.Log("Processing completed for folder: " + propOriginalFolderPath);
    }

   private static void ProcessPrefab(string prefabFile, GameObject baseLandmark, string landmarkPrefabDestinationFolder)
{
    GameObject propPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabFile);
    if (propPrefab == null)
    {
        Debug.LogError("Failed to load prefab at: " + prefabFile);
        return;
    }
    Debug.Log("vertex:fgdgds");

    // Create a new instance of the prop prefab
    GameObject propInstance = Object.Instantiate(propPrefab);
    propInstance.transform.localPosition = Vector3.zero;
    propInstance.name = propPrefab.name; // Ensure the name is correct
    // Add the baseLandmark prefab as a child of the prop instance
    GameObject landmarkInstance = PrefabUtility.InstantiatePrefab(baseLandmark, propInstance.transform) as GameObject;
    if (landmarkInstance != null)
    {
       //  landmarkInstance.name = baseLandmark.name; // Ensure the name is correct
        landmarkInstance.transform.position = Vector3.zero;

        // Ensure the line renderer is at the base of the prop
        LineRenderer lineRenderer = landmarkInstance.GetComponentInChildren<LineRenderer>();
        if (lineRenderer != null)
        {

            // Calculate the lowest point of the propInstance using its mesh
            MeshFilter meshFilter = propInstance.GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                Mesh mesh = meshFilter.sharedMesh;
                if (mesh != null)
                {
                    float lowestPoint = float.MaxValue;
                    foreach (Vector3 vertex in mesh.vertices)
                    {
                        Vector3 worldVertex = propInstance.transform.TransformPoint(vertex);
                        if (worldVertex.y < lowestPoint)
                        {
                            lowestPoint = worldVertex.y;
                        }
                    }

                    // Set the line renderer's position to the lowest point
                    Vector3 lineRendererPosition = new Vector3(propInstance.transform.position.x, lowestPoint, propInstance.transform.position.z);
                    lineRenderer.transform.position = lineRendererPosition;
                }
            }
        }
    }
    else
    {
        Debug.LogError("Failed to instantiate baseLandmark prefab.");
    }

    // Save the modified prefab to the landmarkPrefabDestinationFolder
    string newPrefabPath = Path.Combine(landmarkPrefabDestinationFolder, Path.GetFileNameWithoutExtension(prefabFile) + "Prefab.prefab");
    PrefabUtility.SaveAsPrefabAsset(propInstance, newPrefabPath);

    // Clean up the instantiated objects
    Object.DestroyImmediate(propInstance);
}
}