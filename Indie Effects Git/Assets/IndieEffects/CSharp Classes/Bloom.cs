using UnityEngine;
 
[RequireComponent(typeof(IndieEffectsManager))]
[AddComponentMenu("Indie Effects/C#/Image Bloom")]
public class Bloom : IndieEffect
{
    [Tooltip("TODO : Tooltip")]
    public float threshold;

    [Tooltip("TODO : Tooltip")]
    public float amount;

    [Tooltip("TODO : Tooltip")]
    public Texture2D newTex;
 
    protected override void Init() 
    {
        newTex = new Texture2D(fxRes.textureSize, fxRes.textureSize, TextureFormat.RGB24, false);
        newTex.wrapMode = TextureWrapMode.Clamp;
    }

    private void OnGUI() 
    {
	    effectMat.SetFloat("_Threshold", threshold);
	    effectMat.SetFloat("_Amount", amount);
	    effectMat.SetTexture("_MainTex", fxRes.rt);
	    effectMat.SetTexture("_BlurTex", fxRes.rt);

        base.OnPostRender();
    }
}