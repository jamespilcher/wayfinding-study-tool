using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningPhase : MonoBehaviour
{
    public void InitialiseLearning(GameObject DSP, GameObject RouteEncodingObjects, GameObject trainingRoom)
    {
            DSP.SetActive(true);
            RouteEncodingObjects.SetActive(true);
            trainingRoom.SetActive(false);
    }
}
