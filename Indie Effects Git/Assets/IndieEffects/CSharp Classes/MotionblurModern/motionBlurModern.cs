using UnityEngine;

[AddComponentMenu("Indie Effects/C#/Motion Blur")]
public class motionBlurModern : IndieEffect
{
    // BM : Unused ?
    //public Shader VectorsShader;

    [Range(0.0f, 0.1f)]
    [Tooltip("TODO : Tooltip")]
    public float intensity = 0.001f;

    // BM : Unused ?
    //public Texture2D prevDepth;

    // Give the VPM matrix of the previous frame to the shader
    private Matrix4x4 previousViewProjectionMatrix;

    protected override void Init () 
    {
        previousViewProjectionMatrix = cam.projectionMatrix * cam.worldToCameraMatrix;
    }

    protected override void OnPostRender() 
    {
        Matrix4x4 viewProjection = cam.projectionMatrix * cam.worldToCameraMatrix;
	    //vector map generation
        Matrix4x4 inverseViewProjection = viewProjection.inverse;

        effectMat.SetMatrix("_inverseViewProjectionMatrix" , inverseViewProjection);
        effectMat.SetMatrix("_previousViewProjectionMatrix" , previousViewProjectionMatrix);

        effectMat.SetFloat("_intensity", intensity);

        effectMat.SetTexture("_Depth", fxRes.DNBuffer);

        base.OnPostRender();

        previousViewProjectionMatrix = viewProjection;
    }
}