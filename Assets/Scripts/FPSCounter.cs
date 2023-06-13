using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    // for ui.
    private int screenLongSide;
    private Rect boxRect;
    private GUIStyle style = new GUIStyle();

    // for fps calculation.
    private int frameCount;
    private float elapsedTime;
    private double frameRate;

    public Text FPSValue;

    /// <summary>
    /// Monitor changes in resolution and calcurate FPS
    /// </summary>
    private void Update()
    {
        // FPS calculation
        frameCount++;
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 0.5f)
        {
            frameRate = System.Math.Round(frameCount / elapsedTime, 1, System.MidpointRounding.AwayFromZero);
            frameCount = 0;
            elapsedTime = 0;
                       
            //Send Values to UI Text
            FPSValue.text = frameRate.ToString("F0") + " FPS";
        }
    }

}