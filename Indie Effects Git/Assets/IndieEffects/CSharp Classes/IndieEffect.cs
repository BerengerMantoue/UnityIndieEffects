using UnityEngine;

[RequireComponent(typeof(IndieEffectsManager))]
public abstract class IndieEffect : MonoBehaviour 
{
    private IndieEffectsManager _fxRes = null;
    protected IndieEffectsManager fxRes { get { return _fxRes; } }

    private Material _effectMat = null;
    protected Material effectMat { get { return _effectMat; } }

    private Camera _cam = null;
    protected Camera cam { get { return _cam; } }
    
    [Tooltip("Add the shader for this effect")]
    public Shader effectShader;

	private void Start () 
    {
        useGUILayout = false;

        _fxRes = GetComponent<IndieEffectsManager>();
        if (_fxRes == null)
            Debug.LogError("IndieEffectsManager not found on " + name + " - " + GetType());

        _cam = GetComponent<Camera>();
        if (_cam == null)
            Debug.LogError("Camera not found on " + name);

        if (effectShader == null)
        {
            Debug.LogError("Shader is null on " + name + " - " + GetType());
            _effectMat = new Material("");
        }
        else
            _effectMat = new Material(effectShader);

        Init();
	}

    private void Update()
    {
        if(effectMat != null )
            effectMat.SetTexture("_MainTex", fxRes.rt);

        UpdateEffect();
    }

    protected virtual void OnPostRender()
    {
        IndieEffectTools.FullScreenQuad(_effectMat);
    }

    protected virtual void Init() { }
    protected virtual void UpdateEffect() { }
}
