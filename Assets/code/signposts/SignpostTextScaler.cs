using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Include this if you're working with UI components

[ExecuteInEditMode]
public class SignpostTextScaler : MonoBehaviour
{
    public GameObject sign;
    public GameObject text;

    #if UNITY_EDITOR

    void Update()
    {
        if (Application.isEditor && !Application.isPlaying){
            UpdateTextTransform();
        }
    }

    void UpdateTextTransform()
    {
        if (this == null || !this.isActiveAndEnabled) return; // Check if the object is still valid

        // get the scale of the sign
        Vector3 signScale = sign.transform.localScale;
        // get the rect transform of the text
        RectTransform textTransform = text.GetComponent<RectTransform>();
        if (textTransform != null)
        {
            // set the width and height of the text to be the same as the sign x and y scale
            textTransform.sizeDelta = new Vector2(signScale.x, signScale.y);
            // get the sign transform pos
            Vector3 signPos = sign.transform.localPosition;

            // make the pos z of the text to be negative half of the sign z scale - 0.05f
            text.transform.localPosition = new Vector3(signPos.x, signPos.y, signPos.z - signScale.z / 2 - 0.005f);
        }
    }
    #endif
}
