using UnityEditor;
using UnityEngine;

public class CreateLandmarkPrefabsFromFolderEditor : EditorWindow
{
    private string baseLandmarkPath  = "Assets/Level/Prefabs/Landmarks/BaseLandmark.prefab";
    private string propOriginalFolderPath = "Assets/Props/PropsToBeConvertedToLandmarks";
    private string landmarkPrefabDestinationFolder = "Assets/Level/Prefabs/Landmarks";

    [MenuItem("Tools/Create Landmark Prefabs")]
    public static void ShowWindow()
    {
        GetWindow<CreateLandmarkPrefabsFromFolderEditor>("Create Landmark Prefabs");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create Landmark Prefabs", EditorStyles.boldLabel);

        baseLandmarkPath = EditorGUILayout.TextField("BaseLandmark to be attached Path", baseLandmarkPath);
        propOriginalFolderPath = EditorGUILayout.TextField("Prop Original Folder Path", propOriginalFolderPath);
        landmarkPrefabDestinationFolder = EditorGUILayout.TextField("Landmark Prefab Destination Folder", landmarkPrefabDestinationFolder);

        if (GUILayout.Button("Process Folder"))
        {
            CreateLandmarkPrefabsFromFolder.ProcessFolder(propOriginalFolderPath, baseLandmarkPath, landmarkPrefabDestinationFolder);
        }
    }
}