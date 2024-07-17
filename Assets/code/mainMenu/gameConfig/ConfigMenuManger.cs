using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ConfigMenuManager : MonoBehaviour
{
    public static ConfigMenuManager Instance { get; private set; } // Singleton instance

    public GameObject togglePrefab; // Assign a Toggle prefab in the Inspector
    public GameObject inputFieldPrefab; // Assign an InputField prefab in the Inspector
    public GameObject saveConfigPrefab;
    public Transform configParent; // Assign the parent object for toggles in the Inspector

    private GameConfig gameConfig = new GameConfig();

    private string configSavePath = "Assets/code/mainMenu/gameConfig/SerialisedConfigs/";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep the instance alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy this instance if a duplicate exists
        }
    }

    void Start()
    {
        PopulateConfigMenu();
    }

    void PopulateConfigMenu()
    {
        foreach (FieldInfo field in gameConfig.GetType().GetFields())
        {
            Debug.Log("Field Name: " + field.Name + " | Field Type: " + field.FieldType);
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
                toggle.isOn = (bool)field.GetValue(gameConfig);
                toggle.onValueChanged.AddListener((value) => {
                    field.SetValue(gameConfig, value);
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
                inputField.text = field.GetValue(gameConfig).ToString();
                inputField.onValueChanged.AddListener((value) => {
                    if (float.TryParse(value, out float floatValue))
                    {
                        field.SetValue(gameConfig, floatValue);
                    }
                });
            }
            else
            {
                // Handle other types as needed or disable the object
                // Example: toggleObj.SetActive(false);
            }
        }
        GameObject saveConfigObj = Instantiate(saveConfigPrefab, configParent);
        Button saveConfigButton = saveConfigObj.GetComponent<Button>();
        saveConfigButton.onClick.AddListener(() => {
            saveGameConfigSO(saveConfigObj.GetComponentInChildren<TMP_InputField>().text);
        });
    }

    void saveGameConfigSO(string name){
        if (name == "")
        {
            name = "userConfig";
        }

        GameConfigSO gameConfigSO = ScriptableObject.CreateInstance<GameConfigSO>();
        gameConfigSO.config = gameConfig;
        int i = 1;
        while (System.IO.File.Exists(configSavePath + name + i + ".asset"))
        {
            i++;
        }
        name += i;


        AssetDatabase.CreateAsset(gameConfigSO, configSavePath + name + ".asset");
        AssetDatabase.SaveAssets();
    }
}