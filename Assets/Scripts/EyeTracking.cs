using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeTracking : MonoBehaviour
{
    public GameObject EyeTracker;

    public GameObject SmoothTracker;

    public Text EyeXValue;
    public Text EyeYValue;

    [Tooltip("Movement speed of tracker")] public float SmoothSpeed = 1f;

    [Tooltip("Minimum EyeX Parameter Value in Vtube Studio")] public float XLive2dMin = -1f;
    [Tooltip("Maximum EyeX Parameter Value in Vtube Studio")] public float XLive2dMax = 1f;

    [Tooltip("Minimum EyeY Parameter Value in Vtube Studio")] public float YLive2dMin = -1f;
    [Tooltip("Maximum EyeY Parameter Value in Vtube Studio")] public float YLive2dMax = 1f;

    public bool InvertX;
    public bool InvertY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EyeTracker.transform.position = Vector3.Lerp(SmoothTracker.transform.position, EyeTracker.transform.position, SmoothSpeed);
        
        var X_Min = -5; // Minimum Y Axis 3D Rotation Value in Unity
        var X_Max = 5;  // Maximum Y Axis 3D Rotation Value in Unity

        //var X_Live2d_Min = -1; // Minimum Parameter Value in Live 2D
        //var X_Live2d_Max = 1;  // Maximum Parameter Value in Live 2D

        var X_Live2d_Range = Mathf.Max(XLive2dMin, XLive2dMax) - Mathf.Min(XLive2dMax, XLive2dMin); // Calculate Live2d Range 
        var X_Percentage = (((EyeTracker.transform.localPosition.x - X_Min) * 100f) / (X_Max - X_Min)); //Calculate %
        var X_Calculated = ((X_Percentage * (X_Live2d_Range / 100f)) - (X_Live2d_Range / 2)); //Convert % to Live2d paramater value
        X_Calculated = Mathf.Round(X_Calculated * 100) / 100.0f; //Round Live2d paramater value to 2 decimal places

        //Y

        var Y_Min = -5; // Minimum Y Axis 3D Rotation Value in Unity
        var Y_Max = 5;  // Maximum Y Axis 3D Rotation Value in Unity

        //var Y_Live2d_Min = -1; // Minimum Parameter Value in Live 2D
        //var Y_Live2d_Max = 1;  // Maximum Parameter Value in Live 2D

        var Y_Live2d_Range = Mathf.Max(YLive2dMin, YLive2dMax) - Mathf.Min(YLive2dMax, YLive2dMin); // Calculate Live2d Range 
        var Y_Percentage = (((EyeTracker.transform.localPosition.z - X_Min) * 100f) / (Y_Max - Y_Min)); //Calculate %
        var Y_Calculated = ((Y_Percentage * (Y_Live2d_Range / 100f)) - (Y_Live2d_Range / 2)); //Convert % to Live2d paramater value
        Y_Calculated = Mathf.Round(Y_Calculated * 100) / 100.0f; //Round Live2d paramater value to 2 decimal places

        Y_Calculated = Y_Calculated - Y_Calculated - Y_Calculated; //Needs to be inverted??

        if(InvertX==true)
        {
            X_Calculated = X_Calculated - X_Calculated - X_Calculated;
        }

        if(InvertY == true)
        {
            Y_Calculated = Y_Calculated - Y_Calculated - Y_Calculated;
        }

        //Send Values to VTube Studio
        VTS.Unity.ConnectToVTubeStudio.EyeX = X_Calculated;
        VTS.Unity.ConnectToVTubeStudio.EyeY = Y_Calculated;

        //Send Values to UI Text
        EyeXValue.text = X_Calculated.ToString();
        EyeYValue.text = Y_Calculated.ToString();

    }
}
