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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("DSPTraining");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("DSPLearning");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("DSPTesting");
        }
    }
}