using UnityEngine;

[AddComponentMenu("Indie Effects/C#/Outline")]
public class Outline : IndieEffect
{
    [Tooltip("TODO : Tooltip")]
    public float threshold;

    protected override void UpdateEffect()
    {
        effectMat.SetFloat("_Treshold", threshold);
    }
}