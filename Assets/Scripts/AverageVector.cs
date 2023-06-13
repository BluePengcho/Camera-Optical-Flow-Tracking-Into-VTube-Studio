using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageVector : MonoBehaviour
{
    public GameObject AverageTracker;

    public GameObject CenterTracker;
    public GameObject TopTracker;
    public GameObject BottomTracker;
    public GameObject LeftTracker;
    public GameObject RightTracker;

    public LineRenderer AverageLineRenderer;
    public LineRenderer TopLineRenderer;
    public LineRenderer BottomLineRenderer;
    public LineRenderer LeftLineRenderer;
    public LineRenderer RightLineRenderer;
    public LineRenderer CenterLineRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        //Set width of line renderer
        AverageLineRenderer.startWidth = 0.12f;
        AverageLineRenderer.endWidth = 0.05f;

        TopLineRenderer.startWidth = 0.12f;
        TopLineRenderer.endWidth = 0.05f;

        BottomLineRenderer.startWidth = 0.12f;
        BottomLineRenderer.endWidth = 0.05f;

        LeftLineRenderer.startWidth = 0.12f;
        LeftLineRenderer.endWidth = 0.05f;

        RightLineRenderer.startWidth = 0.12f;
        RightLineRenderer.endWidth = 0.05f;

        CenterLineRenderer.startWidth = 0.12f;
        CenterLineRenderer.endWidth = 0.05f;
    }

    private void Update()
    {

        //Set Tracker Line Renderers
        AverageLineRenderer.SetPosition(1, AverageTracker.transform.localPosition);
        TopLineRenderer.SetPosition(1, TopTracker.transform.localPosition);
        BottomLineRenderer.SetPosition(1, BottomTracker.transform.localPosition);
        LeftLineRenderer.SetPosition(1, LeftTracker.transform.localPosition);
        RightLineRenderer.SetPosition(1, RightTracker.transform.localPosition);
        CenterLineRenderer.SetPosition(1, CenterTracker.transform.localPosition);
        
        //X Tracker 
        var Top_X = TopTracker.transform.localPosition.x;
        var Center_X = CenterTracker.transform.localPosition.x;
        var Bottom_X = BottomTracker.transform.localPosition.x;

        //X Average 
        var Average_X = (Top_X + Center_X + Bottom_X) / 3;

        //Y Tracker
        var Left_Y = LeftTracker.transform.localPosition.y;
        var Center_Y = CenterTracker.transform.localPosition.y;
        var Right_Y = RightTracker.transform.localPosition.y;

        //Y Average 
        var Average_Y = (Left_Y + Center_Y + Right_Y) / 3;
        
        //Set Average Tracker Position
        AverageTracker.transform.localPosition = new Vector3(Average_X, Average_Y, 0);
    }
}
