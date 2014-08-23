using UnityEngine;

[AddComponentMenu("Indie Effects/C#/Vignette")]
public class VignetteBlur : IndieEffect
{
    [Tooltip("TODO : Tooltip")]
    public Texture2D Vignette;
        
    protected override void  UpdateEffect()
    {
        effectMat.SetTexture("_Vignette", Vignette);
    }

    protected override void  OnPostRender()
    {
        base.OnPostRender();

        IndieEffectTools.FullScreenQuad(effectMat, .01f, 1.01f);
        IndieEffectTools.FullScreenQuad(effectMat, .02f, 1.02f);
        IndieEffectTools.FullScreenQuad(effectMat, .04f, 1.04f);
        IndieEffectTools.FullScreenQuad(effectMat, .06f, 1.06f);
        IndieEffectTools.FullScreenQuad(effectMat, .08f, 1.08f);
        IndieEffectTools.FullScreenQuad(effectMat, .1f, 1.1f);
    }
}