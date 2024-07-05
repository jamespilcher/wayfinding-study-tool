using UnityEngine;

public static class LandmarkMappings
{

    public static GameObject GetPrefabForLandmarkType(LandmarkType landmarkType)
    {
            string prefabPath = "art/models/landmarks/";

            switch (landmarkType)
            {
                case LandmarkType.Tree:
                    prefabPath += "TreePrefab";
                    break;
                case LandmarkType.Trashcan:
                    prefabPath += "TrashcanPrefab";
                    break;
                case LandmarkType.Car:
                    prefabPath += "CarPrefab";
                    break;
                default:
                    Debug.LogError("Unsupported landmark type: " + landmarkType);
                    return null;
            }

            GameObject prefab = Resources.Load<GameObject>(prefabPath);

            if (prefab == null)
            {
                Debug.LogError("Prefab not found at path: " + prefabPath);
            }

            return prefab;
        }
    public static LandmarkType GetLandmarkTypeForPrefab(GameObject prefab)
    {
        string prefabPath = prefab.name;

        if (prefabPath.Contains("TreePrefab"))
        {
            return LandmarkType.Tree;
        }
        else if (prefabPath.Contains("TrashcanPrefab"))
        {
            return LandmarkType.Trashcan;
        }
        else if (prefabPath.Contains("CarPrefab"))
        {
            return LandmarkType.Car;
        }
        else
        {
            Debug.LogError("Unsupported prefab: " + prefabPath);
            return LandmarkType.Empty;
        }
    }
}