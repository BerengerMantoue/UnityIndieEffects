using UnityEngine;

[AddComponentMenu("Indie Effects/C#/Chromatic Abberation")]
public class ChromaticAbberation : IndieEffect
{
    [Tooltip("TODO : Tooltip")]
    public Texture2D vignette;

    protected override void OnPostRender ()
    {
	    effectMat.SetTexture("_Vignette", vignette);

        base.OnPostRender();
    }
}