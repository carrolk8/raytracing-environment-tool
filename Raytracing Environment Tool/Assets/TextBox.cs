using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    public string text = "F to advance through route. \nC to reverse through route.";

    void OnGUI() 
    {
        Rect myRect = new Rect(0, 0, 200, 100);
        text = GUI.TextArea(myRect, text);
    }
}
