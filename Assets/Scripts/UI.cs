using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    [Header("Tracking Settings UI")]

    public Button TrackingSettingsButton;
    public GameObject TrackingSettingsWindow;
    public Button TrackingSettingsCloseButton;

    [Header("Video Input Settings UI")]

    public Button VideoInputSettingsButton;
    public GameObject VideoInputSettingsWindow;
    public Button VideoInputSettingsCloseButton;

    public Toggle WebcamToggle;
    public GameObject WebcamInput;
    public GameObject WebcamOptions;

    public InputField WebcamResolutionX;
    public InputField WebcamResolutionY;
    public InputField WebcamFrameRate;

    public Toggle WebcamClampFPSToggle;
    public Toggle WebcamAutoStartToggle;

    public InputField URLInput;
    public Toggle URLAutoConnectToggle;
    public Toggle URLMuteToggle;

    public Toggle RTMPToggle;
    public GameObject RTMPInput;
    public GameObject RTMPOptions;

    public GameObject RTMPURLStatus;

    public GameObject Webcam3dCanvus;
    public GameObject RTMP3dCanvus;

    [Header("Video Output Settings UI")]

    public Button VideoOutputSettingsButton;
    public GameObject VideoOutputSettingsWindow;
    public Button VideoOutputSettingsCloseButton;

    public Toggle SpoutToggle;
    public GameObject SpoutOptions;
    public GameObject SpoutOutput;

    [Header("Video Settings UI")]

    public Button VideoSettingsButton;
    public GameObject VideoSettingsWindow;
    public Button VideoSettingsCloseButton;

    public Toggle FPSTargetToggle;
    public InputField Target_FrameRate;

    [Header("Hide UI Items")]

    public GameObject CenterUILine;
    public GameObject CenterUITracker;
    public GameObject CenterUIBase;

    public GameObject TopUILine;
    public GameObject TopUITracker;
    public GameObject TopUIBase;

    public GameObject BottomUILine;
    public GameObject BottomUITracker;
    public GameObject BottomUIBase;

    public GameObject LeftUILine;
    public GameObject LeftUITracker;
    public GameObject LeftUIBase;

    public GameObject RightUILine;
    public GameObject RightUITracker;
    public GameObject RightUIBase;

    public GameObject AverageUILine;
    public GameObject AverageUITracker;
    public GameObject AverageUIBase;

    public GameObject EyeTrakerUIBlue;
    public GameObject EyeTrakerUIBlack;
    public GameObject EyeTrakerUIWhite;

    [Header("Tracking Settings")]

    public Slider HeadLambdaSlider;
    public Slider HeadThresholdSlider;

    public GameObject HeadLambdaValue;
    public GameObject HeadThresholdValue;

    public Slider EyeLambdaSlider;
    public Slider EyeThresholdSlider;

    public GameObject EyeLambdaValue;
    public GameObject EyeThresholdValue;

    public InputField FaceAngleParameterX_Min;
    public InputField FaceAngleParameterX_Max;
    public InputField FaceAngleParameterY_Min;
    public InputField FaceAngleParameterY_Max;

    public Toggle FaceAngleInvertX;
    public Toggle FaceAngleInvertY;

    public InputField EyeParameterX_Min;
    public InputField EyeParameterX_Max;
    public InputField EyeParameterY_Min;
    public InputField EyeParameterY_Max;

    public Toggle EyeInvertX;
    public Toggle EyeInvertY;

    public Slider EyeSpeedSlider;
    public Slider EyeSmoothingSlider;
    public Slider EyeTrackingQualitySlider;

    [Header("Video Settings")]

    public Toggle SetFrameRateToggle;
    public InputField SetFrameRateInput;

    [Header("Eye Tracking Center Bias")]
    public Slider EyeTrackingCenterBiasSlider;
    public Camera MotionTrackingCamera;


    bool valuechangecounterON;
    int valuechangecounter; 

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 1f;

    public GameObject UICanvas;

    public Toggle VTubeStudioAutoConnectToggle;


    // Start is called before the first frame update
    void Start()
    {
        VTubeStudioAutoConnectToggle.onValueChanged.AddListener(delegate {
            VTubeStudioAutoConnectToggleValueChanged(VTubeStudioAutoConnectToggle);
        });

        EyeTrackingToggle.onValueChanged.AddListener(delegate {
            EyeTrackingToggleValueChanged(EyeTrackingToggle);
        });
        HeadTrackingToggle.onValueChanged.AddListener(delegate {
            HeadTrackingToggleValueChanged(HeadTrackingToggle);
        });

        //Tracking Settings

        TrackingSettingsButton.onClick.AddListener(delegate {
            TrackingSettingsButtonClick();
        });

        TrackingSettingsCloseButton.onClick.AddListener(delegate {
            TrackingSettingsCloseClick();
        });

        //Video Input Settings

        VideoInputSettingsButton.onClick.AddListener(delegate {
            VideoInputSettingsButtonClick();
        });

        VideoInputSettingsCloseButton.onClick.AddListener(delegate {
            VideoInputSettingsCloseClick();
        });


        WebcamToggle.onValueChanged.AddListener(delegate {
            VideoInputValueChange();
        });

        RTMPToggle.onValueChanged.AddListener(delegate {
            VideoInputValueChange();
        });

        RTMPToggle.onValueChanged.AddListener(delegate {
            VideoInputValueChange();
        });

        WebcamResolutionX.onValueChanged.AddListener(delegate {
            WebcamResolutionXValueChange();
        });

        WebcamResolutionY.onValueChanged.AddListener(delegate {
            WebcamResolutionYValueChange();
        });


        WebcamFrameRate.onValueChanged.AddListener(delegate {
            WebcamFrameRateValueChange();
        });

        WebcamClampFPSToggle.onValueChanged.AddListener(delegate {
            WebcamClampFPSToggleValueChange(WebcamClampFPSToggle);
        });

        WebcamAutoStartToggle.onValueChanged.AddListener(delegate {
            WebcamAutoStartToggleValueChange(WebcamAutoStartToggle);
        });

        URLInput.onValueChanged.AddListener(delegate {
            URLInputValueChange();
        });

        URLAutoConnectToggle.onValueChanged.AddListener(delegate {
            URLAutoConnectToggleValueChange(URLAutoConnectToggle);
        });

        URLMuteToggle.onValueChanged.AddListener(delegate {
            URLMuteToggleValueChange(URLMuteToggle);
        });


        //Video Output Settings

        VideoOutputSettingsButton.onClick.AddListener(delegate {
            VideoOutputSettingsButtonClick();
        });

        VideoOutputSettingsCloseButton.onClick.AddListener(delegate {
            VideoOutputSettingsCloseClick();
        });

        SpoutToggle.onValueChanged.AddListener(delegate {
            VideoOutputValueChange();
        });

        HeadLambdaSlider.onValueChanged.AddListener(delegate {
            HeadLambdaChange();
        });

        HeadThresholdSlider.onValueChanged.AddListener(delegate {
            HeadThresholdChange();
        });

        EyeLambdaSlider.onValueChanged.AddListener(delegate {
            EyeLambdaChange();
        });

        EyeThresholdSlider.onValueChanged.AddListener(delegate {
            EyeThresholdChange();
        });

        //Video Settings

        VideoSettingsButton.onClick.AddListener(delegate {
            VideoSettingsButtonClick();
        });

        VideoSettingsCloseButton.onClick.AddListener(delegate {
            VideoSettingsCloseClick();
        });

        FPSTargetToggle.onValueChanged.AddListener(delegate {
            FPS_Toggle();
        });

        Target_FrameRate.onValueChanged.AddListener(delegate {
            FPS_Value_Change();
        });

        SetFrameRateToggle.onValueChanged.AddListener(delegate {
            SetFrameRateToggle_Change();
        });

        SetFrameRateInput.onValueChanged.AddListener(delegate {
            SetFrameRateInput_Change();
        });

        //Tracking Settings

        FaceAngleParameterX_Min.onValueChanged.AddListener(delegate {
            FaceAngleParameterX_Min_Change();
        });

        FaceAngleParameterX_Max.onValueChanged.AddListener(delegate {
            FaceAngleParameterX_Max_Change();
        });

        FaceAngleParameterY_Min.onValueChanged.AddListener(delegate {
            FaceAngleParameterY_Min_Change();
        });

        FaceAngleParameterY_Max.onValueChanged.AddListener(delegate {
            FaceAngleParameterY_Max_Change();
        });

        FaceAngleInvertX.onValueChanged.AddListener(delegate {
            FaceAngleInvertX_Change(FaceAngleInvertX);
        });

        FaceAngleInvertY.onValueChanged.AddListener(delegate {
            FaceAngleInvertY_Change(FaceAngleInvertY);
        });

        EyeParameterX_Min.onValueChanged.AddListener(delegate {
            EyeParameterX_Min_Change();
        });

        EyeParameterX_Max.onValueChanged.AddListener(delegate {
            EyeParameterX_Max_Change();
        });

        EyeParameterY_Min.onValueChanged.AddListener(delegate {
            EyeParameterY_Min_Change();
        });

        EyeParameterY_Max.onValueChanged.AddListener(delegate {
            EyeParameterY_Max_Change();
        });

        EyeInvertX.onValueChanged.AddListener(delegate {
            EyeInvertX_Change(EyeInvertX);
        });

        EyeInvertY.onValueChanged.AddListener(delegate {
            EyeInvertY_Change(EyeInvertY);
        });

        EyeSpeedSlider.onValueChanged.AddListener(delegate {
            EyeSpeedSlider_Change();
        });

        EyeSmoothingSlider.onValueChanged.AddListener(delegate {
            EyeSmoothingSlider_Change();
        });

        EyeTrackingQualitySlider.onValueChanged.AddListener(delegate {
            EyeTrackingQuality_Change();
        });

        //Eye Tracking Center Bias

        MotionTrackingCamera.orthographicSize = Mathf.Round(EyeTrackingCenterBiasSlider.value * 100f) / 100f;

        EyeTrackingCenterBiasSlider.onValueChanged.AddListener(delegate {
            EyeTrackingCenterBiasSliderChange();
        });


    //Get PlayerPrefs Saved Settings

        //UI

            string VtubeStudioAutoConnectToggle_Saved = PlayerPrefs.GetString("VtubeStudioAutoConnectToggle", "On");
            string HeadTrackingToggle_Saved = PlayerPrefs.GetString("HeadTrackingToggle", "On");
            float HeadLambda_Saved = PlayerPrefs.GetFloat("HeadLambda", 1f);
            float HeadThreshold_Saved = PlayerPrefs.GetFloat("HeadThreshold", 1f);
            string EyeTrackingToggle_Saved = PlayerPrefs.GetString("EyeTrackingToggle", "On");
            float EyeLambda_Saved = PlayerPrefs.GetFloat("EyeLambda", 1f);
            float EyeThreshold_Saved = PlayerPrefs.GetFloat("EyeThreshold", 1f);

            //VtubeStudioAutoConnectToggle_Saved

            if (VtubeStudioAutoConnectToggle_Saved == "On")
            {
                VTubeStudioAutoConnectToggle.GetComponent<Toggle>().isOn = true;
            }
            else
            {
                VTubeStudioAutoConnectToggle.GetComponent<Toggle>().isOn = false;
            }

            //HeadTrackingToggle_Set

            if (HeadTrackingToggle_Saved == "On")
            {
                HeadTrackingToggle.GetComponent<Toggle>().isOn = true;
            }
            else
            {
                HeadTrackingToggle.GetComponent<Toggle>().isOn = false;
            }

            //HeadLambda Set

            if (HeadLambda_Saved != 1)
            {
                HeadLambdaSlider.value = HeadLambda_Saved;
            }

            //HeadThreshold Set

            if (HeadThreshold_Saved != 1)
            {
                HeadThresholdSlider.value = HeadThreshold_Saved;
            }

            //EyeTrackingToggle_Set

            if (EyeTrackingToggle_Saved == "On")
            {
                EyeTrackingToggle.GetComponent<Toggle>().isOn = true;
            }
            else
            {
                EyeTrackingToggle.GetComponent<Toggle>().isOn = false;
            }
              
            //EyeLambda Set

            if (EyeLambda_Saved != 1)
            {
                EyeLambdaSlider.value = EyeLambda_Saved;
            }

            //EyeThreshold Set

            if (EyeThreshold_Saved != 1)
            {
                EyeThresholdSlider.value = EyeThreshold_Saved;
            }

        //Video Input

            string WebcamToggle_Saved = PlayerPrefs.GetString("WebcamToggle", "Off");
            //string WebcamDropDown_Saved = PlayerPrefs.GetString("WebcamDropDown"); // - WebcamDrop Down Value purposely not saved
            string WebcamResolutionX_Saved = PlayerPrefs.GetString("WebcamResolutionX");
            string WebcamResolutionY_Saved = PlayerPrefs.GetString("WebcamResolutionY");
            string WebcamFrameRate_Saved = PlayerPrefs.GetString("WebcamFrameRate");
            string WebcamClampFPSToggle_Saved = PlayerPrefs.GetString("WebcamClampFPSToggle", "Off");
            string WebcamAutoStartToggle_Saved = PlayerPrefs.GetString("WebcamAutoStartToggle", "Off");

            string URLToggle_Saved = PlayerPrefs.GetString("URLToggle", "Off");
            string URLInput_Saved = PlayerPrefs.GetString("URLInput", "");
            string URLAutoConnectToggle_Saved = PlayerPrefs.GetString("URLAutoConnectToggle", "Off");
            string URLMuteToggle_Saved = PlayerPrefs.GetString("URLMuteToggle", "Off");

            //WebcamToggle Set

            if (WebcamToggle_Saved == "On")
            {
                WebcamToggle.GetComponent<Toggle>().isOn = true;
            }
            else
            {
                WebcamToggle.GetComponent<Toggle>().isOn = false;
            }

            //WebcamDropDown Set - WebcamDrop Down Value purposely not saved



            //WebcamResolutionX Set

            if (WebcamResolutionX_Saved != "")
                {
                    WebcamResolutionX.text = WebcamResolutionX_Saved;
                }

            //WebcamResolutionY Set

            if (WebcamResolutionY_Saved != "")
            {
                WebcamResolutionY.text = WebcamResolutionY_Saved;
            }

            //WebcamFrameRate Set

            if (WebcamFrameRate_Saved != "")
            {
                WebcamFrameRate.text = WebcamFrameRate_Saved;
            }

            //WebcamClampFPSToggle Set

            if (WebcamClampFPSToggle_Saved == "On")
            {
                WebcamClampFPSToggle.isOn = true;
            }
            else
            {
                WebcamClampFPSToggle.isOn = false;
            }

            //WebcamAutoStartToggle Set

            if (WebcamAutoStartToggle_Saved == "On")
            {
                WebcamAutoStartToggle.isOn = true;
            }
            else
            {
                WebcamAutoStartToggle.isOn = false;
            }

            //URLToggle Set

            if (URLToggle_Saved == "On")
            {
                RTMPToggle.isOn = true;
            }
            else
            {
                RTMPToggle.isOn = false;
            }

            //URLInput Set

            if (URLInput_Saved != "")
            {
                URLInput.text = URLInput_Saved;
            }

            //URLAutoConnectToggle Set

            if (URLAutoConnectToggle_Saved == "On")
            {
                URLAutoConnectToggle.isOn = true;
            }
            else
            {
                URLAutoConnectToggle.isOn = false;
            }

            //URLMuteToggle Set

            if (URLMuteToggle_Saved == "On")
            {
                URLMuteToggle.isOn = true;
            }
            else
            {
                URLMuteToggle.isOn = false;
            }


        //Video Output

            string SpoutToggle_Saved = PlayerPrefs.GetString("SpoutToggle", "Off");

            //SpoutToggle Set

            if (SpoutToggle_Saved == "On")
            {
                SpoutToggle.isOn = true;
            }
            else
            {
                SpoutToggle.isOn = false;
            }


        //TrackingSettings

            string FaceAngleParameterX_Min_Saved = PlayerPrefs.GetString("FaceAngleParameterX_Min");
            string FaceAngleParameterX_Max_Saved = PlayerPrefs.GetString("FaceAngleParameterX_Max");
            string FaceAngleParameterY_Min_Saved = PlayerPrefs.GetString("FaceAngleParameterY_Min");
            string FaceAngleParameterY_Max_Saved = PlayerPrefs.GetString("FaceAngleParameterY_Max");
            string FaceAngleInvertX_Saved = PlayerPrefs.GetString("FaceAngleInvertX", "Off");
            string FaceAngleInvertY_Saved = PlayerPrefs.GetString("FaceAngleInvertY", "Off");

            string EyeParameterX_Min_Saved = PlayerPrefs.GetString("EyeParameterX_Min");
            string EyeParameterX_Max_Saved = PlayerPrefs.GetString("EyeParameterX_Max");
            string EyeParameterY_Min_Saved = PlayerPrefs.GetString("EyeParameterY_Min");
            string EyeParameterY_Max_Saved = PlayerPrefs.GetString("EyeParameterY_Max");
            string EyeInvertX_Saved = PlayerPrefs.GetString("EyeInvertX", "Off");
            string EyeInvertY_Saved = PlayerPrefs.GetString("EyeInvertY", "Off");

            float EyeSpeedSlider_Saved = PlayerPrefs.GetFloat("EyeSpeedSlider", 0);
            float EyeSmoothingSlider_Saved = PlayerPrefs.GetFloat("EyeSmoothingSlider", 1);
            float EyeTrackingQualitySlider_Saved = PlayerPrefs.GetFloat("EyeTrackingQualitySlider", 0);
        
            //FaceAngleParameterX_Min Set

            if (WebcamFrameRate_Saved != "")
            {
                FaceAngleParameterX_Min.text = FaceAngleParameterX_Min_Saved;
            }

            //FaceAngleParameterX_Max Set

            if (FaceAngleParameterX_Max_Saved != "")
            {
                FaceAngleParameterX_Max.text = FaceAngleParameterX_Max_Saved;
            }

            //FaceAngleParameterY_Min Set

            if (FaceAngleParameterY_Min_Saved != "")
            {
                FaceAngleParameterY_Min.text = FaceAngleParameterY_Min_Saved;
            }

            //FaceAngleParameterY_Max Set

            if (FaceAngleParameterY_Max_Saved != "")
            {
                FaceAngleParameterY_Max.text = FaceAngleParameterY_Max_Saved;
            }

            //FaceAngleParameterY Set

            if (FaceAngleParameterY_Max_Saved != "")
            {
                FaceAngleParameterY_Max.text = FaceAngleParameterY_Max_Saved;
            }

            //FaceAngleInvertX Set

            if (FaceAngleInvertX_Saved == "On")
            {
                FaceAngleInvertX.isOn = true;
            }
            else
            {
                FaceAngleInvertX.isOn = false;
            }

            //FaceAngleInvertY Set

            if (FaceAngleInvertY_Saved == "On")
            {
                FaceAngleInvertY.isOn = true;
            }
            else
            {
                FaceAngleInvertY.isOn = false;
            }
        
            //EyeParameterX_Min Set

            if (EyeParameterX_Min_Saved != "")
            {
                EyeParameterX_Min.text = EyeParameterX_Min_Saved;
            }

            //EyeParameterX_Max Set

            if (EyeParameterX_Max_Saved != "")
            {
                EyeParameterX_Max.text = EyeParameterX_Max_Saved;
            }

            //EyeParameterY_Min Set

            if (EyeParameterY_Min_Saved != "")
            {
                EyeParameterY_Min.text = EyeParameterY_Min_Saved;
            }

            //EyeParameterY_Max Set

            if (EyeParameterY_Max_Saved != "")
            {
                EyeParameterY_Max.text = EyeParameterY_Max_Saved;
            }

            //EyeInvertX Set

            if (EyeInvertX_Saved == "On")
            {
                EyeInvertX.isOn = true;
            }
            else
            {
                EyeInvertX.isOn = false;
            }

            //EyeInvertY Set

            if (EyeInvertY_Saved == "On")
            {
                EyeInvertY.isOn = true;
            }
            else
            {
                EyeInvertY.isOn = false;
            }

            //EyeSpeedSlider Set

            if (EyeSpeedSlider_Saved != 0)
            {
                EyeSpeedSlider.value = EyeSpeedSlider_Saved;
            }

            //EyeSmoothingSlider Set

            if (EyeSmoothingSlider_Saved != 1)
            {
                EyeSmoothingSlider.value = EyeSmoothingSlider_Saved;
            }

            //EyeTrackingQualitySlider Set

            if (EyeTrackingQualitySlider_Saved != 0)
            {
                EyeTrackingQualitySlider.value = EyeTrackingQualitySlider_Saved;
            }

        //Video Settings

            string SetFrameRateToggle_Saved = PlayerPrefs.GetString("SetFrameRateToggle", "Off");
            string SetFrameRateInput_Saved = PlayerPrefs.GetString("SetFrameRateInput");
            float EyeTrackingCenterBiasSlider_Saved = PlayerPrefs.GetFloat("EyeTrackingCenterBiasSlider", 0);

            if (SetFrameRateToggle_Saved == "On")
            {
                SetFrameRateToggle.isOn = true;
            }
            else
            {
                SetFrameRateToggle.isOn = false;
            }

            if (SetFrameRateInput_Saved != "")
            {
                SetFrameRateInput.text = SetFrameRateInput_Saved;
            }

            if (EyeTrackingCenterBiasSlider_Saved != 0)
            {
                EyeTrackingCenterBiasSlider.value = EyeTrackingCenterBiasSlider_Saved;
            }

    }

    void VTubeStudioAutoConnectToggleValueChanged(Toggle change)
    {
        if (change.isOn == true)
        {
            PlayerPrefs.SetString("VtubeStudioAutoConnectToggle", "On");
        }
        else
        {
            PlayerPrefs.SetString("VtubeStudioAutoConnectToggle", "Off");
        }
    }

    void EyeTrackingToggleValueChanged(Toggle change)
    {
        if (EyeTracking.activeInHierarchy)
        {
            EyeTracking.SetActive(false);
            EyeValues.SetActive(false);
            PlayerPrefs.SetString("EyeTrackingToggle", "Off");
        }
        else
        {
            EyeTracking.SetActive(true);
            EyeValues.SetActive(true);
            PlayerPrefs.SetString("EyeTrackingToggle", "On");
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
            PlayerPrefs.SetString("HeadTrackingToggle", "Off");
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
            PlayerPrefs.SetString("HeadTrackingToggle", "On");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (valuechangecounterON == true)
        {
            if (valuechangecounter > 50)
            {
                HeadLambdaValue.SetActive(false);
                HeadThresholdValue.SetActive(false);
                EyeLambdaValue.SetActive(false);
                EyeThresholdValue.SetActive(false);
                valuechangecounter = 0;
                valuechangecounterON = false;
            }
            else
            {
                valuechangecounter = valuechangecounter + 1;
            }
        } 
    }

    //Tracking Button

        public void TrackingSettingsButtonClick()
        {
            //UISettings.SetActive(false);
            TrackingSettingsWindow.SetActive(true);
            VideoInputSettingsWindow.SetActive(false);
            VideoOutputSettingsWindow.SetActive(false);
            VideoSettingsWindow.SetActive(false);
        }

        public void TrackingSettingsCloseClick()
        {
            //UISettings.SetActive(true);
            TrackingSettingsWindow.SetActive(false);
        }

        //Video Input Button

        public void VideoInputSettingsButtonClick()
        {
            //UISettings.SetActive(false);
            VideoInputSettingsWindow.SetActive(true);
            TrackingSettingsWindow.SetActive(false);
            VideoOutputSettingsWindow.SetActive(false);
            VideoSettingsWindow.SetActive(false);
        }

        public void VideoInputSettingsCloseClick()
        {
            //UISettings.SetActive(true);
            VideoInputSettingsWindow.SetActive(false);
        }

        public void VideoInputValueChange()
        {
            if ((!RTMPToggle.isOn) && (!WebcamToggle.isOn))
            {
                WebcamOptions.SetActive(false);
                RTMPOptions.SetActive(false);
                WebcamInput.SetActive(false);
                RTMPInput.SetActive(false);

                RTMPURLStatus.SetActive(false);

                Webcam3dCanvus.SetActive(false);
                RTMP3dCanvus.SetActive(false);

                PlayerPrefs.SetString("WebcamToggle", "Off");
                PlayerPrefs.SetString("URLToggle", "Off");
            }

            if (WebcamToggle.isOn)
            {
                WebcamOptions.SetActive(true);
                RTMPOptions.SetActive(false);
                WebcamInput.SetActive(true);
                RTMPInput.SetActive(false);

                RTMPURLStatus.SetActive(false);

                Webcam3dCanvus.SetActive(true);
                RTMP3dCanvus.SetActive(false);

                PlayerPrefs.SetString("WebcamToggle", "On");
                PlayerPrefs.SetString("URLToggle", "Off");
            }
            else
            {
                //WebcamOptions.SetActive(false);
                //RTMPOptions.SetActive(false);
            }

            if (RTMPToggle.isOn)
            {
                WebcamOptions.SetActive(false);
                RTMPOptions.SetActive(true);
                WebcamInput.SetActive(false);
                RTMPInput.SetActive(true);

                RTMPURLStatus.SetActive(true);

                Webcam3dCanvus.SetActive(false);
                RTMP3dCanvus.SetActive(true);

                PlayerPrefs.SetString("WebcamToggle", "Off");
                PlayerPrefs.SetString("URLToggle", "On");
            }
        }

        public void WebcamResolutionXValueChange()
        {
            PlayerPrefs.SetString("WebcamResolutionX", WebcamResolutionX.text);
        }

        public void WebcamResolutionYValueChange()
        {
            PlayerPrefs.SetString("WebcamResolutionY", WebcamResolutionY.text);
        }

        public void WebcamFrameRateValueChange()
        {
            PlayerPrefs.SetString("WebcamFrameRate", WebcamFrameRate.text);
        }

            public void WebcamClampFPSToggleValueChange(Toggle change)
        {
            if (change.isOn == true)
            {
                PlayerPrefs.SetString("WebcamClampFPSToggle", "On");
            }
            else
            {
                PlayerPrefs.SetString("WebcamClampFPSToggle", "Off");
            }
        }

        public void WebcamAutoStartToggleValueChange(Toggle change)
        {
            if (change.isOn == true)
            {
                PlayerPrefs.SetString("WebcamAutoStartToggle", "On");
            }
            else
            {
                PlayerPrefs.SetString("WebcamAutoStartToggle", "Off");
            }
        }

        public void URLInputValueChange()
        {
            PlayerPrefs.SetString("URLInput", URLInput.text);
        }

        public void URLAutoConnectToggleValueChange(Toggle change)
        {
            if (change.isOn == true)
            {
                PlayerPrefs.SetString("URLAutoConnectToggle", "On");
            }
            else
            {
                PlayerPrefs.SetString("URLAutoConnectToggle", "Off");
            }
        }

        public void URLMuteToggleValueChange(Toggle change)
        {
            if (change.isOn == true)
            {
                PlayerPrefs.SetString("URLMuteToggle", "On");
            }
            else
            {
                PlayerPrefs.SetString("URLMuteToggle", "Off");
            }
        }


    //Video Settings Button

    public void VideoSettingsButtonClick()
    {
        //UISettings.SetActive(false);
        VideoSettingsWindow.SetActive(true);
        VideoOutputSettingsWindow.SetActive(false);
        TrackingSettingsWindow.SetActive(false);
        VideoInputSettingsWindow.SetActive(false);
    }

    public void VideoSettingsCloseClick()
    {
        //UISettings.SetActive(true);
        VideoSettingsWindow.SetActive(false);
    }

    public void FPS_Toggle()
    {
        if(FPSTargetToggle.isOn == true)
        {
        Application.targetFrameRate = int.Parse(Target_FrameRate.text);
        }
        else
        {
        Application.targetFrameRate = -1;
        }
    }

    public void FPS_Value_Change()
    {
        if (FPSTargetToggle.isOn == true)
        {
            Application.targetFrameRate = int.Parse(Target_FrameRate.text);
        }
    }

    public void FaceAngleParameterX_Min_Change()
    {
        PlayerPrefs.SetString("FaceAngleParameterX_Min", FaceAngleParameterX_Min.text);
    }

    public void FaceAngleParameterX_Max_Change()
    {
        PlayerPrefs.SetString("FaceAngleParameterX_Max", FaceAngleParameterX_Max.text);
    }

    public void FaceAngleParameterY_Min_Change()
    {
        PlayerPrefs.SetString("FaceAngleParameterY_Min", FaceAngleParameterY_Min.text);
    }

    public void FaceAngleParameterY_Max_Change()
    {
        PlayerPrefs.SetString("FaceAngleParameterY_Max", FaceAngleParameterY_Max.text);
    }

    public void FaceAngleInvertX_Change(Toggle change)
    {
        if (change.isOn == true)
        {
            PlayerPrefs.SetString("FaceAngleInvertX", "On");
        }
        else
        {
            PlayerPrefs.SetString("FaceAngleInvertX", "Off");
        }
    }

    public void FaceAngleInvertY_Change(Toggle change)
    {
        if (change.isOn == true)
        {
            PlayerPrefs.SetString("FaceAngleInvertY", "On");
        }
        else
        {
            PlayerPrefs.SetString("FaceAngleInvertY", "Off");
        }
    }

    public void EyeParameterX_Min_Change()
    {
        PlayerPrefs.SetString("EyeParameterX_Min", EyeParameterX_Min.text);
    }

    public void EyeParameterX_Max_Change()
    {
        PlayerPrefs.SetString("EyeParameterX_Max", EyeParameterX_Max.text);
    }

    public void EyeParameterY_Min_Change()
    {
        PlayerPrefs.SetString("EyeParameterY_Min", EyeParameterY_Min.text);
    }

    public void EyeParameterY_Max_Change()
    {
        PlayerPrefs.SetString("EyeParameterY_Max", EyeParameterY_Max.text);
    }

    public void EyeInvertX_Change(Toggle change)
    {
        if (change.isOn == true)
        {
            PlayerPrefs.SetString("EyeInvertX", "On");
        }
        else
        {
            PlayerPrefs.SetString("EyeInvertX", "Off");
        }
    }

    public void EyeInvertY_Change(Toggle change)
    {
        if (change.isOn == true)
        {
            PlayerPrefs.SetString("EyeInvertY", "On");
        }
        else
        {
            PlayerPrefs.SetString("EyeInvertY", "Off");
        }
    }

    public void EyeSpeedSlider_Change()
    {
        PlayerPrefs.SetFloat("EyeSpeedSlider", EyeSpeedSlider.value);
    }

    public void EyeSmoothingSlider_Change()
    {
        PlayerPrefs.SetFloat("EyeSmoothingSlider", EyeSmoothingSlider.value);
    }

    public void EyeTrackingQuality_Change()
    {
        PlayerPrefs.SetFloat("EyeTrackingQualitySlider", EyeTrackingQualitySlider.value);
    }

    //Video Output Button

        public void VideoOutputSettingsButtonClick()
            {
                //UISettings.SetActive(false);
                VideoOutputSettingsWindow.SetActive(true);
                TrackingSettingsWindow.SetActive(false);
                VideoInputSettingsWindow.SetActive(false);
                VideoSettingsWindow.SetActive(false);
        }

        public void VideoOutputSettingsCloseClick()
        {
            //UISettings.SetActive(true);
            VideoOutputSettingsWindow.SetActive(false);
        }
        
        public void HeadLambdaChange()
        {
            HeadLambdaValue.SetActive(true);
            valuechangecounterON = true;
            valuechangecounter = 0;
            PlayerPrefs.SetFloat("HeadLambda", HeadLambdaSlider.value);
        }

        public void HeadThresholdChange()
        {
            HeadThresholdValue.SetActive(true);
            valuechangecounterON = true;
            valuechangecounter = 0;
            PlayerPrefs.SetFloat("HeadThreshold", HeadThresholdSlider.value);
        }

        public void EyeLambdaChange()
        {
            EyeLambdaValue.SetActive(true);
            valuechangecounterON = true;
            valuechangecounter = 0;
            PlayerPrefs.SetFloat("EyeLambda", EyeLambdaSlider.value);
        }

        public void EyeThresholdChange()
        {
            EyeThresholdValue.SetActive(true);
            valuechangecounterON = true;
            valuechangecounter = 0;
            PlayerPrefs.SetFloat("EyeThreshold", EyeThresholdSlider.value);
        }


        public void VideoOutputValueChange()
        {
            if (SpoutToggle.isOn)
            {
                SpoutOptions.SetActive(true);
                SpoutOutput.SetActive(true);
                PlayerPrefs.SetString("SpoutToggle", "On");
            }
            else
            {
                SpoutOptions.SetActive(false);
                SpoutOutput.SetActive(false);
                PlayerPrefs.SetString("SpoutToggle", "Off");
            }
        }



    public void SetFrameRateInput_Change()
    {
        PlayerPrefs.SetString("SetFrameRateInput", SetFrameRateInput.text);
    }

    public void SetFrameRateToggle_Change()
    {
        if (SetFrameRateToggle.isOn)
        {
            PlayerPrefs.SetString("SetFrameRateToggle", "On");
        }
        else
        {
            PlayerPrefs.SetString("SetFrameRateToggle", "Off");
        }
    }

    public void EyeTrackingCenterBiasSliderChange()
    {
         MotionTrackingCamera.orthographicSize = Mathf.Round(EyeTrackingCenterBiasSlider.value * 100f) / 100f;
         PlayerPrefs.SetFloat("EyeTrackingCenterBiasSlider", EyeTrackingCenterBiasSlider.value);
    }
    
        public void UIDoubleClick()
        {
            clicked++;

            if (clicked == 1)
            {
                clicktime = Time.time;
            }
       
            if (clicked > 1 && Time.time - clicktime < clickdelay)
            {
                clicked = 0;
                clicktime = 0;
            
                if (UICanvas.activeInHierarchy)
                {
                    UICanvas.SetActive(false);

                    CenterUILine.SetActive(false);
                    CenterUITracker.GetComponent<MeshRenderer>().enabled = false;
                    CenterUIBase.GetComponent<MeshRenderer>().enabled = false;

                    TopUILine.SetActive(false);
                    TopUITracker.GetComponent<MeshRenderer>().enabled = false;
                    TopUIBase.GetComponent<MeshRenderer>().enabled = false;

                    BottomUILine.SetActive(false);
                    BottomUITracker.GetComponent<MeshRenderer>().enabled = false;
                    BottomUIBase.GetComponent<MeshRenderer>().enabled = false;

                    LeftUILine.SetActive(false);
                    LeftUITracker.GetComponent<MeshRenderer>().enabled = false;
                    LeftUIBase.GetComponent<MeshRenderer>().enabled = false;

                    RightUILine.SetActive(false);
                    RightUITracker.GetComponent<MeshRenderer>().enabled = false;
                    RightUIBase.GetComponent<MeshRenderer>().enabled = false;

                    AverageUILine.SetActive(false);
                    AverageUITracker.GetComponent<MeshRenderer>().enabled = false;
                    AverageUIBase.GetComponent<MeshRenderer>().enabled = false;

                    EyeTrakerUIBlue.GetComponent<MeshRenderer>().enabled = false;
                    EyeTrakerUIWhite.GetComponent<MeshRenderer>().enabled = false;
                    EyeTrakerUIBlack.GetComponent<MeshRenderer>().enabled = false;
}
                else
                {
                    UICanvas.SetActive(true);

                    CenterUILine.SetActive(true);
                    CenterUITracker.GetComponent<MeshRenderer>().enabled = true;
                    CenterUIBase.GetComponent<MeshRenderer>().enabled = true;

                    TopUILine.SetActive(true);
                    TopUITracker.GetComponent<MeshRenderer>().enabled = true;
                    TopUIBase.GetComponent<MeshRenderer>().enabled = true;

                    BottomUILine.SetActive(true);
                    BottomUITracker.GetComponent<MeshRenderer>().enabled = true;
                    BottomUIBase.GetComponent<MeshRenderer>().enabled = true;

                    LeftUILine.SetActive(true);
                    LeftUITracker.GetComponent<MeshRenderer>().enabled = true;
                    LeftUIBase.GetComponent<MeshRenderer>().enabled = true;

                    RightUILine.SetActive(true);
                    RightUITracker.GetComponent<MeshRenderer>().enabled = true;
                    RightUIBase.GetComponent<MeshRenderer>().enabled = true;

                    AverageUILine.SetActive(true);
                    AverageUITracker.GetComponent<MeshRenderer>().enabled = true;
                    AverageUIBase.GetComponent<MeshRenderer>().enabled = true;

                    EyeTrakerUIBlue.GetComponent<MeshRenderer>().enabled = true;
                    EyeTrakerUIWhite.GetComponent<MeshRenderer>().enabled = true;
                    EyeTrakerUIBlack.GetComponent<MeshRenderer>().enabled = true;
                }
            }
            else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;

        }

}
