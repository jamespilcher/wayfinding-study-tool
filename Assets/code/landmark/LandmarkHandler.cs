using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmarkHandler : MonoBehaviour
{
    public LandmarkType landmarkType;
    private GameObject currentModel;

    void Start()
    {
        UpdateModel();
    }
    void OnValidate()
    {
        #if UNITY_EDITOR
        UpdateModel();
        #endif
    }
    // Method to update the model based on the selected landmarkType
    public void UpdateModel()
    {
        // Destroy current model if it exists
        if (currentModel != null)
        {
            #if UNITY_EDITOR
            DestroyImmediate(currentModel);
            #endif
        }

        // Get the prefab for the selected landmarkType
        GameObject modelPrefab = LandmarkMappings.GetPrefabForLandmarkType(landmarkType);

        if (modelPrefab == null)
        {
            Debug.LogError("Prefab not found for landmark type: " + landmarkType);
            return;
        }

        // Instantiate the model prefab
        currentModel = Instantiate(modelPrefab, transform);
        currentModel.name = modelPrefab.name; // Set the name to the model's name
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
