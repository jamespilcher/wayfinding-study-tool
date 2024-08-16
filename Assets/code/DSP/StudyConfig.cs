using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyConfig
{
    private static StudyConfig _instance;

    public static StudyConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new StudyConfig();
            }
            return _instance;
        }
    }

    private StudyConfig()
    {
        // Private constructor to prevent instantiation from outside
    }

    public string studyName = "DSP";
    public string participantID = "";
    public bool shuffleDSPPaths = false;  // implemented
    // Add other configuration settings as needed
}