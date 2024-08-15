using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StudyConfig", menuName = "ScriptableObjects/StudyConfig", order = 1)]
public class StudyConfig : ScriptableObject
{
    public string studyName = "DSP";
    public string participantID = "a";
    public bool shuffleDSPPaths = true;  // implemented
    public bool hasVisibleTimer = false;  
    // Add other configuration settings as needed
}