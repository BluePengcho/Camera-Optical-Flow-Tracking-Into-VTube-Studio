using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyWebcamInput : MonoBehaviour
{
    [SerializeField] string deviceName = "";
    [SerializeField] Vector2Int resolution;
    int frameRate = 60;
    public InputField input_resolutionX;
    public InputField input_resolutionY;
    public InputField input_frameRate;    
    WebCamTexture webcam;
    [SerializeField] public RenderTexture targetBuffer;
    public Toggle clampFPSToCameraFPS;
    [SerializeField] public Dropdown m_Dropdown;
    public Toggle WebcamToggle;
    public Toggle AutoStartCamera;
    public Button StartButton;
    public Button EndButton;

    void Start()
    {
        resolution.x = int.Parse(input_resolutionX.text);
        resolution.y = int.Parse(input_resolutionY.text);

        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged();
        });

        input_resolutionX.onValueChanged.AddListener(delegate {
            Values_Change();
        });

        input_resolutionY.onValueChanged.AddListener(delegate {
            Values_Change();
        });

        input_frameRate.onValueChanged.AddListener(delegate {
            Values_Change();
        });

        clampFPSToCameraFPS.onValueChanged.AddListener(delegate {
            Values_Change();
        });

        WebcamToggle.onValueChanged.AddListener(delegate {
            VideoInputValueChange();
        });

        StartButton.onClick.AddListener(delegate {
            Start_Camera();
        });

        EndButton.onClick.AddListener(delegate {
            Stop_Camera();
        });

        deviceName = m_Dropdown.options[m_Dropdown.value].text;
        
        if (deviceName == "" || deviceName == null)
        {
            //Debug.Log("No Camera Set");
        }
        else
        {
            if (clampFPSToCameraFPS.isOn)
            {
                // ! Only usefull if you want to clamp the FPS to the speed of youir camera 
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = frameRate;
            }

            if(AutoStartCamera.isOn == true)
            {
            webcam = new WebCamTexture(deviceName, resolution.x, resolution.y, frameRate);
            webcam.Play();
            }

        }

}

    public void VideoInputValueChange()
    {
        if (WebcamToggle.isOn)
        {
            
        }
        else
        {
            m_Dropdown.value = m_Dropdown.options.FindIndex(option => option.text == "");
        }

    }

    void OnDestroy()
    {
        if (webcam != null) Destroy(webcam);
    }

    void Update()
    {
        if (webcam != null)
        {
            if (!webcam.didUpdateThisFrame) return;

            var aspect1 = (float)webcam.width / webcam.height;
            var aspect2 = (float)resolution.x / resolution.y;
            var gap = aspect2 / aspect1;

            var vflip = webcam.videoVerticallyMirrored;
            var scale = new Vector2(gap, vflip ? -1 : 1);
            var offset = new Vector2((1 - gap) / 2, vflip ? 1 : 0);

            Graphics.Blit(webcam, targetBuffer, scale, offset);
        }
    }

    public void DropdownValueChanged()
    {
        if (webcam != null)
        {
            webcam.Stop();
        }

        deviceName = m_Dropdown.options[m_Dropdown.value].text;

        if (deviceName == "" || deviceName == null)
        {
            //Debug.Log("No Camera Set");
        }
        else
        {
            if (clampFPSToCameraFPS.isOn)
            {
                // ! Only usefull if you want to clamp the FPS to the speed of youir camera 
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = frameRate;
            }

            if (AutoStartCamera.isOn == true)
            {
                webcam = new WebCamTexture(deviceName, resolution.x, resolution.y, frameRate);
                webcam.Play();
            }
        }

    }

    public void RestartCamera()
    {
        if (webcam != null)
        {
            webcam.Stop();
        }

        deviceName = m_Dropdown.options[m_Dropdown.value].text;

        if (deviceName == "" || deviceName == null)
        {
            //Debug.Log("No Camera Set");
        }
        else
        {
            if (clampFPSToCameraFPS.isOn)
            {
                // ! Only usefull if you want to clamp the FPS to the speed of youir camera 
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = frameRate;
            }

            if (AutoStartCamera.isOn == true)
            {
                webcam = new WebCamTexture(deviceName, resolution.x, resolution.y, frameRate);
                webcam.Play();
            }
        }


    }

    public void Start_Camera()
    {
        if (webcam != null)
        {
            webcam.Stop();
        }

        deviceName = m_Dropdown.options[m_Dropdown.value].text;

        if (deviceName == "" || deviceName == null)
        {
            //Debug.Log("No Camera Set");
        }
        else
        {
            if (clampFPSToCameraFPS.isOn)
            {
                // ! Only usefull if you want to clamp the FPS to the speed of youir camera 
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = frameRate;
            }
            webcam = new WebCamTexture(deviceName, resolution.x, resolution.y, frameRate);
            webcam.Play();
        }
    }

    public void Stop_Camera()
    {
        if (webcam != null)
        {
            webcam.Stop();
            Destroy(webcam);
        }
    }

    public void Values_Change()
    {
        int number;

        if (int.TryParse(input_resolutionX.text, out number)) //Check if string is a viable number 
        {
            resolution.x = int.Parse(input_resolutionX.text);
        }
        if (int.TryParse(input_resolutionY.text, out number)) //Check if string is a viable number 
        {
            resolution.y = int.Parse(input_resolutionY.text);
        }

        if (int.TryParse(input_frameRate.text, out number)) //Check if string is a viable number 
        {
            frameRate = int.Parse(input_frameRate.text);
        }

        RestartCamera();
    }


}
