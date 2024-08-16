using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using System.Reflection;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DSPMainMenu : MonoBehaviour
{
    public StudyConfig studyConfig = StudyConfig.Instance;
    public GameObject togglePrefab;
    public GameObject inputFieldPrefab;
    public Transform canvas;


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
                GameObject inputObj = Instantiate(inputFieldPrefab, canvas);
                TMP_InputField inputField = inputObj.GetComponent<TMP_InputField>();
                TMP_Text label = inputObj.transform.Find("Label").GetComponent<TMP_Text>();
                if (label != null)
                {
                    label.text = ObjectNames.NicifyVariableName(field.Name);
                }
                inputField.text = field.GetValue(studyConfig).ToString();
                inputField.onValueChanged.AddListener((value) => {
                    if (field.FieldType == typeof(float))
                    {
                        if (float.TryParse(value, out float floatValue))
                        {
                            field.SetValue(studyConfig, floatValue);
                        }
                    }
                    else if (field.FieldType == typeof(string))
                    {
                        field.SetValue(studyConfig, value);
                    }
                });
            }
            else
            {
                // Handle other types as needed or disable the object
            }
        }
    }


    public void beginStudy(){
        SceneManager.LoadScene("DSPTraining");
    }
}