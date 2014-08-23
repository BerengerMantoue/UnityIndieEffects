using UnityEngine;

[AddComponentMenu("Indie Effects/C#/Depth of Field")]
public class DepthOfField : IndieEffect
{
    [Range(0, 10)]
    [Tooltip("TODO : Tooltip")]
    public float FStop;

    [Range(0, 5)]
    [Tooltip("TODO : Tooltip")]
    public float BlurAmount;

    protected override void OnPostRender() 
    {
        effectMat.SetTexture("_Depth", fxRes.DNBuffer);
        effectMat.SetFloat("_FStop", FStop * 10);
        effectMat.SetFloat("_Amount", BlurAmount);

        base.OnPostRender();
    }
}