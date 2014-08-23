using UnityEngine;
using System.Collections;

/*
----------Indie Effects Base----------
This is the base for all other image effects to occur. Includes depth texture generation
*/

[RequireComponent(typeof(Camera))]
[AddComponentMenu("Indie Effects/C#/IndieEffectsBase")]
public class IndieEffectsManager : MonoBehaviour
{
    // Base effects
    private Texture2D _rt;
    public Texture2D rt { get { return _rt; } }
    
    /// <summary>
    /// textureSize
    /// </summary>
    [Tooltip("TODO : Tooltip")]
    public int textureSize;
    
    [Tooltip("TODO : Tooltip")]
    public Shader DepthShader;


    [Tooltip("TODO : Tooltip")]
    public bool DNRequire;

    public Texture2D DNBuffer { get; set; }
    public GameObject DepthCam { get; set; }

    [Range(0f, 0.04f)]
    [Tooltip("TODO : Tooltip")]
    public float latency;

    [Tooltip("TODO : Tooltip")]
    public bool useOldVersion; //enable old rendering method

    private Camera myCamera; //cache camera and transform component for
    private Transform myTransform; //performance        
    private GameObject dom;

    public void OnPreRender () 
    {
        if( !useOldVersion )
        {		
            float asp = camera.pixelWidth/camera.pixelHeight;
            if (!DepthCam)
            {
                DepthCam = new GameObject("DepthCamera", typeof(Camera));
                DepthCam.camera.CopyFrom(camera);
                DepthCam.camera.depth = camera.depth-2;
            }
            if (!dom)
            {
                dom = new GameObject("capture", typeof(Camera));
                dom.camera.CopyFrom(camera);
                dom.camera.depth = camera.depth-3;
            }
            if (DNRequire) 
            {
                DepthCam.transform.position = camera.transform.position;
                DepthCam.transform.rotation = camera.transform.rotation;
                DepthCam.camera.SetReplacementShader(DepthShader, "RenderType");
                DepthCam.camera.aspect = asp;
                DepthCam.camera.pixelRect = new Rect(0,0,textureSize,textureSize);
                DepthCam.camera.Render();
                DNBuffer.ReadPixels(new Rect(0, 0, textureSize, textureSize), 0, 0);
                DNBuffer.Apply();
            }

            dom.transform.position = camera.transform.position;
            dom.transform.rotation = camera.transform.rotation;
            dom.camera.aspect = asp;
            dom.camera.pixelRect = new Rect(0, 0, textureSize, textureSize);
            dom.camera.Render();
            rt.ReadPixels(new Rect(camera.pixelRect.x, camera.pixelRect.y, textureSize, textureSize), 0, 0);
            rt.Apply();
	
        } 
        else 
        {
            if( DNRequire ) 
            {
                //for old rendering method
                if (!DepthCam){
                    DepthCam = new GameObject("DepthCamera", typeof(Camera));
                    DepthCam.camera.CopyFrom(myCamera);
                    DepthCam.camera.depth = myCamera.depth-2;
                }

                DepthCam.transform.position = myTransform.position;
                DepthCam.transform.rotation = myTransform.rotation;
                DepthCam.camera.SetReplacementShader(DepthShader, "RenderType");
                DepthCam.camera.aspect = myCamera.aspect;
                DepthCam.camera.pixelRect = myCamera.pixelRect;
                DepthCam.camera.Render();
                DNBuffer.Resize(Mathf.RoundToInt(myCamera.pixelWidth), Mathf.RoundToInt(myCamera.pixelHeight), TextureFormat.RGB24, false);
                DNBuffer.ReadPixels(myCamera.pixelRect, 0, 0);
                DNBuffer.Apply();
		
            }
		
        }
    }

    public void OnPostRender() 
    {
        if( useOldVersion )
        {
            rt.Resize(Mathf.RoundToInt(myCamera.pixelWidth), Mathf.RoundToInt(myCamera.pixelHeight), TextureFormat.RGB24, false);
            rt.ReadPixels(myCamera.pixelRect, 0, 0);
	   
            rt.Apply();	
        }
    }

    public IEnumerator Start () 
    {	
        // Check for causes of errors
        if (UnityEditor.PlayerSettings.useDirect3D11)
            Debug.LogWarning("Dx11 is enabled, this will cause several problems with the Indie Effects package, such as the pink screen.\nPlease disable it in your player settings.");

        myTransform = transform;
        myCamera = GetComponent<Camera>();
	
        if( !useOldVersion )
        {		
            _rt = new Texture2D(textureSize, textureSize, TextureFormat.RGB24, false);
            rt.wrapMode = TextureWrapMode.Clamp;
            DNBuffer = new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false);
            DNBuffer.wrapMode = TextureWrapMode.Clamp;

            while(Application.isPlaying)
                yield return new WaitForSeconds(latency);
	
        } else 
        {
            //old approch
            //possible for splitscreen too
            _rt = new Texture2D(Mathf.RoundToInt(myCamera.pixelWidth), Mathf.RoundToInt(myCamera.pixelHeight), TextureFormat.RGB24, false);

            //RT.wrapMode = TextureWrapMode.Clamp;
            DNBuffer = new Texture2D(Mathf.RoundToInt(myCamera.pixelWidth), Mathf.RoundToInt(myCamera.pixelHeight), TextureFormat.ARGB32, false);
            yield break;
        }
    }
}