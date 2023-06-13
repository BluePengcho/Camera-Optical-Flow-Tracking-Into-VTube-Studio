using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadTracking : MonoBehaviour
{
    public GameObject Tracker;

    public Text HeadXValue;
    public Text HeadYValue;

    public InputField HeadXMin_input;
    public InputField HeadXMax_input;

    public InputField HeadYMin_input;
    public InputField HeadYMax_input;


    [Tooltip("Minimum HeadX Parameter Value in Vtube Studio")] public float XLive2dMin;
    [Tooltip("Maximum HeadX Parameter Value in Vtube Studio")] public float XLive2dMax;

    [Tooltip("Minimum HeadY Parameter Value in Vtube Studio")] public float YLive2dMin;
    [Tooltip("Maximum HeadY Parameter Value in Vtube Studio")] public float YLive2dMax;

    public Toggle InvertX;
    public Toggle InvertY;

    // Start is called before the first frame update
    void Start()
    {
        Live2dValues_Change();

        HeadXMin_input.onValueChanged.AddListener(delegate {
            Live2dValues_Change();
        });

        HeadXMax_input.onValueChanged.AddListener(delegate {
            Live2dValues_Change();
        });

        HeadYMin_input.onValueChanged.AddListener(delegate {
            Live2dValues_Change();
        });

        HeadYMax_input.onValueChanged.AddListener(delegate {
            Live2dValues_Change();
        });

    }

    // Update is called once per frame
    void Update()
    {
 
        var X_Min = -5; // Minimum Y Axis 3D Rotation Value in Unity
        var X_Max = 5;  // Maximum Y Axis 3D Rotation Value in Unity

        var X_Live2d_Range = Mathf.Max(XLive2dMin, XLive2dMax) - Mathf.Min(XLive2dMax, XLive2dMin); // Calculate Live2d Range 
        var X_Percentage = (((Tracker.transform.localPosition.x - X_Min) * 100f) / (X_Max - X_Min)); //Calculate %
        var X_Calculated = ((X_Percentage * (X_Live2d_Range / 100f)) - (X_Live2d_Range / 2)); //Convert % to Live2d paramater value
        X_Calculated = Mathf.Round(X_Calculated * 100) / 100.0f; //Round Live2d paramater value to 2 decimal places

        //Y

        var Y_Min = -5; // Minimum Y Axis 3D Rotation Value in Unity
        var Y_Max = 5;  // Maximum Y Axis 3D Rotation Value in Unity

        var Y_Live2d_Range = Mathf.Max(YLive2dMin, YLive2dMax) - Mathf.Min(YLive2dMax, YLive2dMin); // Calculate Live2d Range 
        var Y_Percentage = (((Tracker.transform.localPosition.y - X_Min) * 100f) / (Y_Max - Y_Min)); //Calculate %
        var Y_Calculated = ((Y_Percentage * (Y_Live2d_Range / 100f)) - (Y_Live2d_Range / 2)); //Convert % to Live2d paramater value
        Y_Calculated = Mathf.Round(Y_Calculated * 100) / 100.0f; //Round Live2d paramater value to 2 decimal places

        if (InvertX.isOn == true)
        {
            X_Calculated = X_Calculated - X_Calculated - X_Calculated;
        }

        if (InvertY.isOn == true)
        {
            Y_Calculated = Y_Calculated - Y_Calculated - Y_Calculated;
        }

        //Send Values to VTube Studio
        VTS.Unity.ConnectToVTubeStudio.FaceX = X_Calculated;
        VTS.Unity.ConnectToVTubeStudio.FaceY = Y_Calculated;

        //Send Values to UI Text
        HeadXValue.text = X_Calculated.ToString();
        HeadYValue.text = Y_Calculated.ToString();

    }

    public void Live2dValues_Change()
    {
        int number;

        if(int.TryParse(HeadXMin_input.text, out number)) //Check if string is a viable number 
        {
            XLive2dMin = int.Parse(HeadXMin_input.text);
        }

        if (int.TryParse(HeadXMax_input.text, out number)) //Check if string is a viable number 
        {
            XLive2dMax = int.Parse(HeadXMax_input.text);
        }

        if (int.TryParse(HeadYMin_input.text, out number)) //Check if string is a viable number 
        {
            YLive2dMin = int.Parse(HeadYMin_input.text);
        }

        if (int.TryParse(HeadYMax_input.text, out number)) //Check if string is a viable number 
        {
            YLive2dMax = int.Parse(HeadYMax_input.text);
        }

    }
    
}
