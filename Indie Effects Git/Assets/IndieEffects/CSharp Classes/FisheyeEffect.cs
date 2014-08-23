using UnityEngine;

/* 
						---Fisheye Image Effect---
This Indie Effects script is an adaption of the Unity Pro Fisheye Effect done by me.
If you want me to attempt to convert a unity pro image effect, consult the manual for my
forum link and email address.
*/

[AddComponentMenu("Indie Effects/C#/Fisheye")]
public class FisheyeEffect : IndieEffect
{    
    [Tooltip("TODO : Tooltip")]
    public float strengthX = 0.2f;

    [Tooltip("TODO : Tooltip")]
    public float strengthY = 0.2f;

    [Tooltip("TODO : Tooltip")]
    private Texture2D tex;

    private const float ONE_OVER_BASE_SIZE = 80.0f / 512.0f;
    
    protected override void OnPostRender() 
    {				
        float ar = Screen.width / Screen.height;

        effectMat.SetVector("intensity", new Vector4(   strengthX * ar * ONE_OVER_BASE_SIZE,
                                                        strengthY * ONE_OVER_BASE_SIZE,
                                                        strengthX * ar * ONE_OVER_BASE_SIZE,
                                                        strengthY * ONE_OVER_BASE_SIZE));

        base.OnPostRender();
    }
}