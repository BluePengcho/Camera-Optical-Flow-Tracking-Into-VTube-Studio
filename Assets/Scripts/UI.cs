using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject EyeTracking;
    public Toggle EyeTrackingToggle;
    public GameObject EyeValues;

    public Toggle HeadTrackingToggle;
    public GameObject HeadTracking;
    public GameObject HeadValues;

    public GameObject CenterTrackerUI;
    public GameObject TopTrackerUI;
    public GameObject BottomTrackerUI;
    public GameObject LeftTrackerUI;
    public GameObject RightTrackerUI;



    // Start is called before the first frame update
    void Start()
    {
        EyeTrackingToggle.onValueChanged.AddListener(delegate {
            EyeTrackingToggleValueChanged(EyeTrackingToggle);
        });
        HeadTrackingToggle.onValueChanged.AddListener(delegate {
            HeadTrackingToggleValueChanged(HeadTrackingToggle);
        });
    }
        
    void EyeTrackingToggleValueChanged(Toggle change)
    {
        if (EyeTracking.activeInHierarchy)
        {
            EyeTracking.SetActive(false);
            EyeValues.SetActive(false);
        }
        else
        {
            EyeTracking.SetActive(true);
            EyeValues.SetActive(true);
        }
    }

    void HeadTrackingToggleValueChanged(Toggle change)
    {
        if (HeadTracking.activeInHierarchy)
        {
            HeadTracking.SetActive(false);
            HeadValues.SetActive(false);
            CenterTrackerUI.SetActive(false);
            TopTrackerUI.SetActive(false);
            BottomTrackerUI.SetActive(false);
            LeftTrackerUI.SetActive(false);
            RightTrackerUI.SetActive(false);
        }
        else
        {
            HeadTracking.SetActive(true);
            HeadValues.SetActive(true);
            CenterTrackerUI.SetActive(true);
            TopTrackerUI.SetActive(true);
            BottomTrackerUI.SetActive(true);
            LeftTrackerUI.SetActive(true);
            RightTrackerUI.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
