using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maximumScript : MonoBehaviour
{
    public ComputeShader shader;
    public Texture2D inputTexture;
    
    public uint[] groupMaxData;
    public int groupMax;

    private ComputeBuffer groupMaxBuffer;

    private int handleMaximumMain;

    public GameObject Tracker;
    public GameObject SmoothTracker;
    [Tooltip("Movement speed of tracker")] public float SmoothSpeed = 1f;

    public GameObject image;

    public RenderTexture RenderTexture;

    public float SetDistance = 1.5f;


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
        inputTexture = toTexture2D(RenderTexture, RenderTexture.width, RenderTexture.height);

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
      
        // This gets the shared material used by this renderer. If there are other renderers that use the same material, changing this material will also affect them.
        //Material sharedMaterial = renderer.sharedMaterial;

        shader.Dispatch(handleMaximumMain, (inputTexture.height + 63) / 64, 1, 1);
        // divided by 64 in x because of [numthreads(64,1,1)] in the compute shader code
        // added 63 to make sure that there is a group for all rows

        // get maxima of groups
        groupMaxBuffer.GetData(groupMaxData);

        // find maximum of all groups
        groupMax = 0;
        for (int group = 1; group < (inputTexture.height + 63) / 64; group++)
        {


            if (groupMaxData[3 * group + 2] > groupMaxData[3 * groupMax + 2])
            {
                groupMax = group;

                var my_x = image.transform.localScale.x;
                var my_y = image.transform.localScale.z;

                image.transform.localPosition = new Vector3(inputTexture.width/2, inputTexture.height/2, 0);
                image.transform.localScale = new Vector3(inputTexture.width/10, 1, inputTexture.height/10);

                Tracker.transform.localPosition = Vector3.Lerp(Tracker.transform.localPosition, new Vector3(groupMaxData[3 * groupMax + 0], groupMaxData[3 * groupMax + 1], -100), Time.deltaTime * SmoothSpeed);
            }
            
        }

        float distance = Vector3.Distance(SmoothTracker.transform.position, Tracker.transform.position);

        if (distance > SetDistance)
        {
            SmoothTracker.transform.localPosition = Vector3.Lerp(SmoothTracker.transform.localPosition, Tracker.transform.localPosition, Time.deltaTime * SmoothSpeed);
        }

        Destroy(inputTexture);
        groupMaxBuffer.Release();
    }
}
