using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StudyPhaseManager : MonoBehaviour
{
    public static StudyPhaseManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // keys 1,2,3 to switch between scenes
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SceneManager.LoadScene("DSPTraining");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SceneManager.LoadScene("DSPLearning");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene("DSPTesting");
        }
    }
}