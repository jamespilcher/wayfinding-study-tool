using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

using System.Reflection;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DSPMainMenu : MonoBehaviour
{
    public GameObject togglePrefab; // Assign a Toggle prefab in the Inspector
    public GameObject inputFieldPrefab; // Assign an InputField prefab in the Inspector
    public StudyConfig studyConfig;
    public Transform canvas; // Assign the parent object for toggles in the Inspector


    void Start()
    {
        PopulateConfigMenu();
    }

    void PopulateConfigMenu()
    {
        foreach (FieldInfo field in studyConfig.GetType().GetFields())
        {
            Debug.Log("Field Name: " + field.Name + " | Field Type: " + field.FieldType);
            if (field.FieldType == typeof(bool))
            {
                // Instantiate a toggle for bool fields
                GameObject toggleObj = Instantiate(togglePrefab, canvas);
                Toggle toggle = toggleObj.GetComponent<Toggle>();
                Text label = toggle.GetComponentInChildren<Text>();
                if (label != null)
                {
                    label.text = ObjectNames.NicifyVariableName(field.Name);
                }
                toggle.isOn = (bool)field.GetValue(studyConfig);
                toggle.onValueChanged.AddListener((value) => {
                    field.SetValue(studyConfig, value);
                });
            }
            else if (field.FieldType == typeof(float) || field.FieldType == typeof(string))
            {
                // Instantiate an input field for float fields
                GameObject inputObj = Instantiate(inputFieldPrefab, canvas);
                TMP_InputField inputField = inputObj.GetComponent<TMP_InputField>();
                TMP_Text label = inputObj.transform.Find("Label").GetComponent<TMP_Text>();
                if (label != null)
                {
                    label.text = ObjectNames.NicifyVariableName(field.Name);
                }
                inputField.text = field.GetValue(studyConfig).ToString();
                inputField.onValueChanged.AddListener((value) => {
                    if (float.TryParse(value, out float floatValue))
                    {
                        field.SetValue(studyConfig, floatValue);
                    }
                });
            }
            // else if (field.FieldType == typeof(int))
            // {
            //     // Instantiate an input field for int fields
            //     GameObject inputObj = Instantiate(inputFieldPrefab, canvas);
            //     TMP_InputField inputField = inputObj.GetComponent<TMP_InputField>();
            //     TMP_Text label = inputObj.transform.Find("Label").GetComponent<TMP_Text>();
            //     if (label != null)
            //     {
            //         label.text = ObjectNames.NicifyVariableName(field.Name);
            //     }
            //     inputField.text = field.GetValue(studyConfig).ToString();
            //     inputField.onValueChanged.AddListener((value) => {
            //         if (int.TryParse(value, out int intValue))
            //         {
            //             field.SetValue(studyConfig, intValue);
            //         }
            //     });
            // }
            else
            {
                // Handle other types as needed or disable the object
                // Example: toggleObj.SetActive(false);
            }
        }
    }

    void saveGameConfigSO(string name){
        // if (name == "")
        // {
        //     name = "userConfig";
        // }

        // GameConfigSO studyConfigSO = ScriptableObject.CreateInstance<GameConfigSO>();
        // studyConfigSO.config = studyConfig;
        // int i = 1;
        // while (System.IO.File.Exists(configSavePath + name + i + ".asset"))
        // {
        //     i++;
        // }
        // name += i;


        // AssetDatabase.CreateAsset(studyConfigSO, configSavePath + name + ".asset");
        // AssetDatabase.SaveAssets();
    }
}