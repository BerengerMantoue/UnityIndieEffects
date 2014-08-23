using UnityEngine;

/*
	CameraMotionBlur.js
	This effect creates a motion blur effect when the camera is moved.
	Object motion blur is with this not possible, but it gives a good impression
	of speed. The effect is a bit wobbly, I think the bigger the depth texture 
	resolution is the stabler the effect (not confirmed). Please change the intensity for your needs
	
	~0tacun
*/
 
[AddComponentMenu("Indie Effects/C#/CameraMotionBlur")]
public class CameraMotionBlur : IndieEffect
{
    [Tooltip("TODO : Tooltip")]
    public float intensity = 0.05f;

    // Give the VPM matrix of the previous frame to the shader
    private Matrix4x4 previousViewProjectionMatrix;
 
    protected override void  Init()
    {
	    previousViewProjectionMatrix = fxRes.DepthCam.camera.projectionMatrix * fxRes.DepthCam.camera.worldToCameraMatrix;
    }

 
    protected override void OnPostRender () 
    { 
	    Matrix4x4 viewProjection = fxRes.DepthCam.camera.projectionMatrix * fxRes.DepthCam.camera.worldToCameraMatrix;
        Matrix4x4 inverseViewProjection = viewProjection.inverse; 
 
	    effectMat.SetMatrix("_inverseViewProjectionMatrix" , inverseViewProjection);
        effectMat.SetMatrix("_previousViewProjectionMatrix", previousViewProjectionMatrix);

        effectMat.SetFloat("_intensity", intensity);

        effectMat.SetTexture("_MainTex", fxRes.rt);
        effectMat.SetTexture("_CameraDepthTexture", fxRes.DNBuffer);

        base.OnPostRender();
   
	    previousViewProjectionMatrix = viewProjection;
    }
}