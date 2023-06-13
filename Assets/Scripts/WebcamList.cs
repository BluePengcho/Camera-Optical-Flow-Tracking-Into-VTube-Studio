using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WebcamList : MonoBehaviour
{
    public Dropdown m_Dropdown;

    // Gets the list of devices
    void Start()
    {
         //Create a List of new Dropdown options
        List<string> m_DropOptions = new List<string> { };

        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            m_DropOptions.Add(devices[i].name);
            //Debug.Log(devices[i].name);
        }

        //Add the options created in the List above
        m_Dropdown.AddOptions(m_DropOptions);
    }

    public void RefreshList()
    {
        //Create a List of new Dropdown options
        List<string> m_DropOptions = new List<string> {""};

        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            m_DropOptions.Add(devices[i].name);
        }

        //Clear the old options of the Dropdown menu
        m_Dropdown.ClearOptions();
        //Add the options created in the List above
        m_Dropdown.AddOptions(m_DropOptions);
    }

}
