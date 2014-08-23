using UnityEngine;

// Here is a Bunell Disk Screen Space Ambient Occlusion or Disk to Disk SSAO implementation, ported in Unity Free by Cyrien5100 (me).
// Original technique was developped by Arkano22 based on an Nvidia Implementaion, but in geometry space.
// Arkano22 modified it to work in Screen Space.
// About me, i translated the shader in CG (original was GLSL), and i tweaked/customized it a little, to work correctly in Unity.
// If you use it in your games, please say my name in credits ;)
// Big thanks to Arkano22 for creating this EPIC technique, to FuzzyQuills for IndieEffects, to 0tacun for helping in position reconstruction, 
// to #Include Graphics and bwhiting from GameDev forum for helping me about self occlusion problem.
[AddComponentMenu("Indie Effects/C#/Screen Space Ambient Occlusion")]
public class SSAO_C5100 : IndieEffect
{
    [Tooltip("TODO : Tooltip")]
    public Texture2D randTex;

    [Tooltip("TODO : Tooltip")]
    public float bias = 1;

    [Tooltip("TODO : Tooltip")]
    public float samplingRadius= 1.0f;

    [Tooltip("TODO : Tooltip")]
    public float scale= 0.8f;

    [Range(2, 8)]
    [Tooltip("TODO : Tooltip")]
    public int iterations = 3;

    [Range(0.7f, 0.9f)]
    [Tooltip("TODO : Tooltip")]
    public float selfOcclusion = 0.8f;

    [Tooltip("TODO : Tooltip")]
    public float strength = 2.0f;
    
    protected override void OnPostRender()
    {
        base.OnPostRender();

        //	depthTex = _NormalsDepth.DepthTex;
        effectMat.SetTexture("_DepthNormalTex", fxRes.DNBuffer);
        effectMat.SetTexture("_noiseTex", randTex);
        effectMat.SetFloat("_Bias", -bias);
        effectMat.SetFloat("_scale", scale);
        effectMat.SetFloat("_sampleRad", samplingRadius * 100);
        effectMat.SetInt("_iterations", iterations);
        effectMat.SetFloat("_selfOcclusion", selfOcclusion);
        effectMat.SetFloat("_strength", strength);

        Matrix4x4 P = camera.projectionMatrix;
        Matrix4x4 invP = P.inverse;
        invP = invP.transpose;

        effectMat.SetMatrix("_InvProj", invP); // Set the 4x4 Matrix
    }
}