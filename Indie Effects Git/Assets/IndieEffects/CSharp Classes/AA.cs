using UnityEngine;

/*
---------- Anti-Aliasing Indie Effects----------

This is an adaption of Unity Pro's AA Script, done by TheBlur (me)

*/
[AddComponentMenu("Indie Effects/C#/Anti-Aliasing")]
public class AA : IndieEffect
{
    public enum AAMode {
	    FXAA2 = 0,
	    FXAA3Console = 1,		
	    FXAA1PresetA = 2,
	    FXAA1PresetB = 3,
	    NFAA = 4,
	    SSAA = 5,
	    DLAA = 6,
    }

	public AAMode mode = AAMode.FXAA3Console;

	public bool showGeneratedNormals = false;
	public float offsetScale = 0.2f;
	public float blurRadius = 18f;

	public float edgeThresholdMin = 0.05f;
	public float edgeThreshold = 0.2f;
	public float edgeSharpness  = 4.0f;
		
	public bool dlaaSharp = false;
        
	public Shader ssaaShader;
	private Material ssaa;
	public Shader dlaaShader;
	private Material dlaa;
	public Shader nfaaShader;
	private Material nfaa;	
	public Shader shaderFXAAPreset2;
	private Material materialFXAAPreset2;
	public Shader shaderFXAAPreset3;
	private Material materialFXAAPreset3;
	public Shader shaderFXAAII;
	private Material materialFXAAII;
	public Shader shaderFXAAIII;
	private Material materialFXAAIII;
		
	public Material CurrentAAMaterial ()
	{
		Material returnValue = null;

		switch(mode) {
			case AAMode.FXAA3Console:
				returnValue = materialFXAAIII;
				break;
			case AAMode.FXAA2:
				returnValue = materialFXAAII;
				break;
			case AAMode.FXAA1PresetA:
				returnValue = materialFXAAPreset2;
				break;
			case AAMode.FXAA1PresetB:
				returnValue = materialFXAAPreset3;
				break;
			case AAMode.NFAA:
				returnValue = nfaa;
				break;
			case AAMode.SSAA:
				returnValue = ssaa;
				break;
			case AAMode.DLAA:
				returnValue = dlaa;
				break;	
			default:
				returnValue = null;
				break;
			}
			
		return returnValue;
	}

	protected override void  Init()
    {
		materialFXAAPreset2 = new Material (shaderFXAAPreset2);
		materialFXAAPreset3 = new Material (shaderFXAAPreset3);
		materialFXAAII = new Material (shaderFXAAII);
		materialFXAAIII = new Material (shaderFXAAIII);
		nfaa = new Material (nfaaShader);
		ssaa = new Material (ssaaShader); 
		dlaa = new Material (dlaaShader); 	            
	}
	
	protected override void  UpdateEffect()
    {   
	    materialFXAAPreset2.mainTexture = fxRes.rt;
	    materialFXAAPreset3.mainTexture = fxRes.rt;
	    materialFXAAII.mainTexture = fxRes.rt;
	    materialFXAAIII.mainTexture = fxRes.rt;
	    nfaa.mainTexture = fxRes.rt;
	    ssaa.mainTexture = fxRes.rt;
	    dlaa.mainTexture = fxRes.rt;
	}

    protected override void OnPostRender() 
    {

 		// .............................................................................
		// FXAA antialiasing modes .....................................................}

		if (mode == AAMode.FXAA3Console && (materialFXAAIII != null)) 
        {
			materialFXAAIII.SetFloat("_EdgeThresholdMin", edgeThresholdMin);
			materialFXAAIII.SetFloat("_EdgeThreshold", edgeThreshold);
			materialFXAAIII.SetFloat("_EdgeSharpness", edgeSharpness);		
		
            IndieEffectTools.FullScreenQuad(materialFXAAIII);
        }        
		else if (mode == AAMode.FXAA1PresetB && (materialFXAAPreset3 != null)) {
            IndieEffectTools.FullScreenQuad(materialFXAAPreset3);
        }
        else if(mode == AAMode.FXAA1PresetA && materialFXAAPreset2 != null) {
            fxRes.rt.anisoLevel = 4;
            IndieEffectTools.FullScreenQuad(materialFXAAPreset2);
            fxRes.rt.anisoLevel = 0;
        }
        else if(mode == AAMode.FXAA2 && materialFXAAII != null) {
            IndieEffectTools.FullScreenQuad(materialFXAAII);
        }
		else if (mode == AAMode.SSAA && ssaa != null) {

		// .............................................................................
		// SSAA antialiasing ...........................................................
			
			IndieEffectTools.FullScreenQuad(ssaa);								
		}
		else if (mode == AAMode.DLAA && dlaa != null) {

		// .............................................................................
		// DLAA antialiasing ...........................................................
		
			fxRes.rt.anisoLevel = 0;	
			IndieEffectTools.FullScreenQuad(dlaa);			
			IndieEffectTools.FullScreenQuad(dlaa);					
		}
		else if (mode == AAMode.NFAA && nfaa != null) {

		// .............................................................................
		// nfaa antialiasing ..............................................
			
			fxRes.rt.anisoLevel = 0;	
		
			nfaa.SetFloat("_OffsetScale", offsetScale);
			nfaa.SetFloat("_BlurRadius", blurRadius);
				
			IndieEffectTools.FullScreenQuad(nfaa);					
		}
		else {
			// none of the AA is supported, fallback to a simple blit
			IndieEffectTools.FullScreenQuad(null);
		}
	}
}