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

    public InputField EyeXMin_input;
    public InputField EyeXMax_input;

    public InputField EyeYMin_input;
    public InputField EyeYMax_input;

    [Tooltip("Movement speed of tracker")] public Slider SmoothSpeed;

    [Tooltip("Minimum EyeX Parameter Value in Vtube Studio")] public float XLive2dMin;
    [Tooltip("Maximum EyeX Parameter Value in Vtube Studio")] public float XLive2dMax;

    [Tooltip("Minimum EyeY Parameter Value in Vtube Studio")] public float YLive2dMin;
    [Tooltip("Maximum EyeY Parameter Value in Vtube Studio")] public float YLive2dMax;

    public Toggle InvertX;
    public Toggle InvertY;

    // Start is called before the first frame update
    void Start()
    {

        Live2dValues_Change();

        EyeXMin_input.onValueChanged.AddListener(delegate {
            Live2dValues_Change();
        });

        EyeXMax_input.onValueChanged.AddListener(delegate {
            Live2dValues_Change();
        });

        EyeYMin_input.onValueChanged.AddListener(delegate {
            Live2dValues_Change();
        });

        EyeYMax_input.onValueChanged.AddListener(delegate {
            Live2dValues_Change();
        });

    }

    // Update is called once per frame
    void Update()
    {
        EyeTracker.transform.position = Vector3.Lerp(SmoothTracker.transform.position, EyeTracker.transform.position, SmoothSpeed.value);
        
        var X_Min = -5; // Minimum Y Axis 3D Rotation Value in Unity
        var X_Max = 5;  // Maximum Y Axis 3D Rotation Value in Unity

        var X_Live2d_Range = Mathf.Max(XLive2dMin, XLive2dMax) - Mathf.Min(XLive2dMax, XLive2dMin); // Calculate Live2d Range 
        var X_Percentage = (((EyeTracker.transform.localPosition.x - X_Min) * 100f) / (X_Max - X_Min)); //Calculate %
        var X_Calculated = ((X_Percentage * (X_Live2d_Range / 100f)) - (X_Live2d_Range / 2)); //Convert % to Live2d paramater value
        X_Calculated = Mathf.Round(X_Calculated * 100) / 100.0f; //Round Live2d paramater value to 2 decimal places

        //Y

        var Y_Min = -5; // Minimum Y Axis 3D Rotation Value in Unity
        var Y_Max = 5;  // Maximum Y Axis 3D Rotation Value in Unity

        var Y_Live2d_Range = Mathf.Max(YLive2dMin, YLive2dMax) - Mathf.Min(YLive2dMax, YLive2dMin); // Calculate Live2d Range 
        var Y_Percentage = (((EyeTracker.transform.localPosition.z - X_Min) * 100f) / (Y_Max - Y_Min)); //Calculate %
        var Y_Calculated = ((Y_Percentage * (Y_Live2d_Range / 100f)) - (Y_Live2d_Range / 2)); //Convert % to Live2d paramater value
        Y_Calculated = Mathf.Round(Y_Calculated * 100) / 100.0f; //Round Live2d paramater value to 2 decimal places

        Y_Calculated = Y_Calculated - Y_Calculated - Y_Calculated; //Needs to be inverted??

        if(InvertX.isOn == true)
        {
            X_Calculated = X_Calculated - X_Calculated - X_Calculated;
        }

        if(InvertY.isOn == true)
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

    public void Live2dValues_Change()
    {
        int number;

        if (int.TryParse(EyeXMin_input.text, out number)) //Check if string is a viable number 
        {
            XLive2dMin = int.Parse(EyeXMin_input.text);
        }

        if (int.TryParse(EyeXMax_input.text, out number)) //Check if string is a viable number 
        {
            XLive2dMax = int.Parse(EyeXMax_input.text);
        }

        if (int.TryParse(EyeYMin_input.text, out number)) //Check if string is a viable number 
        {
            YLive2dMin = int.Parse(EyeYMin_input.text);
        }

        if (int.TryParse(EyeYMax_input.text, out number)) //Check if string is a viable number 
        {
            YLive2dMax = int.Parse(EyeYMax_input.text);
        }

    }
}
