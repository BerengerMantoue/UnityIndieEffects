using UnityEngine;

/*
This script is intended for use with Fuzzy Quill's True Motion Blur for Unity free script.
Shader variable is intended for the ColorBalance shader that should have been provided
along with this script.

-Adam T. Ryder
http://1337atr.weebly.com
*/

[AddComponentMenu("Indie Effects/C#/Color Balance")]
public class ColorBalance : IndieEffect
{
    [Tooltip("TODO : Tooltip")]
    public Color Lift = Color.white;

    [Tooltip("TODO : Tooltip")]
    public float LiftBright = 1f;

    [Tooltip("TODO : Tooltip")]
    public Color Gamma = Color.white;

    [Tooltip("TODO : Tooltip")]
    public float GammaBright = 1f;

    [Tooltip("TODO : Tooltip")]
    public Color Gain  = Color.white;

    [Tooltip("TODO : Tooltip")]
    public float GainBright = 1f;

    protected override void  Init()
    {
	    effectMat.SetColor("_Lift", Lift);
	    effectMat.SetFloat("_LiftB", Mathf.Clamp(LiftBright, 0f, 2f));
	    effectMat.SetColor("_Gamma", Gamma);
	    effectMat.SetFloat("_GammaB", Mathf.Clamp(GammaBright, 0f, 2f));
	    effectMat.SetColor("_Gain", Gain);
	    effectMat.SetFloat("_GainB", Mathf.Clamp(GainBright, 0f, 2f));
    }
}