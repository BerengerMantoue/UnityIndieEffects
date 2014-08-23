using UnityEngine;

[AddComponentMenu("Indie Effects/C#/Blur")]
public class Blur : IndieEffect
{
    [Range(0, 5)]
    [Tooltip("TODO : Tooltip")]
    public float blur;
    
    protected override void  Init()
    {
        effectMat.SetFloat("_Amount", blur);
    }
}