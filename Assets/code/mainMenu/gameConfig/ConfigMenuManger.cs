using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class ConfigMenuManager : MonoBehaviour
{
    public GameConfigSO gameConfigSO; // Assign this in the inspector
    public GameObject togglePrefab; // Assign a Toggle prefab in the Inspector
    public GameObject inputFieldPrefab; // Assign an InputField prefab in the Inspector
    public Transform configParent; // Assign the parent object for toggles in the Inspector

    void Start()
    {
        // Clone the GameConfigSO to create a temporary runtime configuration
        PopulateConfigMenu();
    }

    void PopulateConfigMenu()
    {
        foreach (FieldInfo field in gameConfigSO.GetType().GetFields())
    {
        Debug.Log(field.Name);
        if (field.FieldType == typeof(bool))
        {
            // Instantiate a toggle for bool fields
            GameObject toggleObj = Instantiate(togglePrefab, configParent);
            Toggle toggle = toggleObj.GetComponent<Toggle>();
            Text label = toggle.GetComponentInChildren<Text>();
            if (label != null)
            {
                label.text = ObjectNames.NicifyVariableName(field.Name);
            }
            toggle.isOn = (bool)field.GetValue(gameConfigSO.config);
            toggle.onValueChanged.AddListener((value) => {
                field.SetValue(gameConfigSO.config, value);
            });
        }
        else if (field.FieldType == typeof(float))
        {
            // Instantiate an input field for float fields
            GameObject inputObj = Instantiate(inputFieldPrefab, configParent);
            TMP_InputField inputField = inputObj.GetComponent<TMP_InputField>();
            TMP_Text label = inputObj.transform.Find("Label").GetComponent<TMP_Text>();
            if (label != null)
            {
                label.text = ObjectNames.NicifyVariableName(field.Name);
            }
            inputField.text = field.GetValue(gameConfigSO.config).ToString();
            inputField.onValueChanged.AddListener((value) => {
                if (float.TryParse(value, out float floatValue))
                {
                    field.SetValue(gameConfigSO.config, floatValue);
                }
            });
        }
        else
        {
            // Handle other types as needed or disable the object
            // Example: toggleObj.SetActive(false);
        }
    }
    }
}