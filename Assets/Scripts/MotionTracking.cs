using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotionTracking : MonoBehaviour
{
    public ComputeShader shader;
    Texture2D inputTexture;
    public uint[] groupMaxData;
    int groupMax;
    private ComputeBuffer groupMaxBuffer;
    private int handleMaximumMain;
    public GameObject EyeTrackingAnchor;
    public GameObject Tracker;
    public GameObject SmoothTracker;
    //[Tooltip("Movement speed of tracker")] public float SmoothSpeed = 1f;
    [Tooltip("Movement speed of tracker")] public Slider SmoothSpeed;
    //[Tooltip("Analize Movement every # of Frames")] public float FrameSkip = 3;
    [Tooltip("Analize Movement every # of Frames")] public Slider FrameSkip;
    Vector3 mynewvector = new Vector3(0,0,0);
    public RenderTexture InputRenderTexture;
    public float SetDistance = 1.5f;
    public Camera MotionTrackingCamera;

    void Start()
    {

    }

    void OnDestroy()
    {
        if (null != groupMaxBuffer)
        {
            groupMaxBuffer.Release();
        }
    }

    Texture2D toTexture2D(RenderTexture rTex, int width, int height)
    {
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    void Update()
    {
        Tracker.transform.localPosition = Vector3.Lerp(Tracker.transform.localPosition, mynewvector, Time.deltaTime * SmoothSpeed.value);

        if (Time.frameCount % FrameSkip.value == 0)//This will be only executed each # of frames
        {
            inputTexture = toTexture2D(InputRenderTexture, InputRenderTexture.width, InputRenderTexture.height);

            handleMaximumMain = shader.FindKernel("MaximumMain");
            groupMaxBuffer = new ComputeBuffer((inputTexture.height + 63) / 64, sizeof(uint) * 3);
            groupMaxData = new uint[((inputTexture.height + 63) / 64) * 3];

            if (handleMaximumMain < 0 || null == groupMaxBuffer || null == groupMaxData)
            {
                Debug.Log("Initialization failed.");
                return;
            }

            shader.SetTexture(handleMaximumMain, "InputTexture", inputTexture);
            shader.SetInt("InputTextureWidth", inputTexture.width);
            shader.SetBuffer(handleMaximumMain, "GroupMaxBuffer", groupMaxBuffer);


            shader.Dispatch(handleMaximumMain, (inputTexture.height + 63) / 64, 1, 1);
            // divided by 64 in x because of [numthreads(64,1,1)] in the compute shader code
            // added 63 to make sure that there is a group for all rows

            // get maxima of groups
            groupMaxBuffer.GetData(groupMaxData);

            // find maximum of all groups
            groupMax = 0;

            for (int group = 1; group < (inputTexture.height + 63) / 64; group++)
            {
                float camerasize = MotionTrackingCamera.orthographicSize;

                float GetCameraSize = 2.5f / camerasize; //2.5f is the defult camera size

                if (groupMaxData[3 * group + 2] < groupMaxData[3 * groupMax + 2])
                {
                    mynewvector = new Vector3(groupMaxData[3 * groupMax + 0], groupMaxData[3 * groupMax + 1], -100);

                    mynewvector.x = mynewvector.x * 4; //x4 to compensate for the 4 times smaller processed render texture used for performance improvement
                    mynewvector.y = mynewvector.y * 4; //x4 to compensate for the 4 times smaller processed render texture used for performance improvement

                    EyeTrackingAnchor.transform.localPosition = new Vector3((5 - (5/ GetCameraSize)), (5 - (5 / GetCameraSize)), 0); //Recalibrate Eye Tracking anchor position to match Eye Tracking Center Bias Setting

                    mynewvector = mynewvector / GetCameraSize;

                    Tracker.transform.localPosition = Vector3.Lerp(Tracker.transform.localPosition, mynewvector, Time.deltaTime * SmoothSpeed.value);
                }

                if (groupMaxData[3 * group + 2] > groupMaxData[3 * groupMax + 2])
                {
                    groupMax = group;
                    mynewvector = new Vector3(groupMaxData[3 * groupMax + 0], groupMaxData[3 * groupMax + 1], -100);

                    mynewvector.x = mynewvector.x * 4; //x4 to compensate for the 4 times smaller processed render texture used for performance improvement
                    mynewvector.y = mynewvector.y * 4; //x4 to compensate for the 4 times smaller processed render texture used for performance improvement

                    EyeTrackingAnchor.transform.localPosition = new Vector3((5 - (5 / GetCameraSize)), (5 - (5 / GetCameraSize)), 0); //Recalibrate Eye Tracking anchor position to match Eye Tracking Center Bias Setting

                    mynewvector = mynewvector / GetCameraSize;

                    Tracker.transform.localPosition = Vector3.Lerp(Tracker.transform.localPosition, mynewvector, Time.deltaTime * SmoothSpeed.value);
                }
            }
            Destroy(inputTexture);
            groupMaxBuffer.Release();
        }

        float distance = Vector3.Distance(SmoothTracker.transform.position, Tracker.transform.position);

            if (distance > SetDistance)
            {
                SmoothTracker.transform.localPosition = Vector3.Lerp(SmoothTracker.transform.localPosition, Tracker.transform.localPosition, Time.deltaTime * SmoothSpeed.value);
            }

    }
}
