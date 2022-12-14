using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    public enum EffectType
    {
        Phase,
        Dissolve,
    }

    #region Public Fields


    [SerializeField]
    Renderer[] renderers;
    [SerializeField]
    Material[] matOrgins;
    [SerializeField]
    Material[] matDissolves;
    [SerializeField]
    Material[] matPhases;
    [SerializeField]
    float targetHeight = 2.0f;
    [SerializeField]
    float fadeTime = 2.0f;
    [SerializeField]
    EffectType effectType;
    #endregion


    #region Private Fields
    #endregion


    #region MonoBehaviour Callbacks
    private void Start()
    {
        switch(effectType)
        {
            case EffectType.Phase:
            {
                LoopMaterial( renderers, matPhases );
                DoFade( 0, targetHeight, fadeTime );
            }
            break;
            case EffectType.Dissolve:
            {
                LoopMaterial( renderers, matDissolves );
                DoFade( 0, targetHeight, fadeTime );
            }
            break;
        }

    }
    private void Update()
    {
	
    }
    #endregion


    #region Public Methods
    #endregion


    #region Private Methods

    void LoopMaterial(Renderer[] renderers, Material[] materials)
    {
        for(var i = 0; i < renderers.Length; ++i )
        {
            renderers[i].material = materials[i];
        }
    }

    void DoFade(float start, float dest, float time)
    {
        iTween.ValueTo( gameObject,
            iTween.Hash(
                "from", start,
                "to", dest,
                "time", time,
                "onupdatetarget", gameObject,
                "onupdate", "TweenOnUpdate",
                "oncomplete", "TweenOnComplete",
                "easetype", iTween.EaseType.easeInOutCubic
                )
            );
    }
    void TweenOnUpdate(float value)
    {
        //render.material.SetFloat( "_Split_Value", value );
        for ( var i = 0; i < renderers.Length; ++i )
        {
            renderers[i].material.SetFloat( "_Split_Value", value );
        }

    }
    void TweenOnComplete()
    {
        //render.material = matOrgin;
        LoopMaterial( renderers, matOrgins );
    }

    #endregion
}
