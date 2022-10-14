using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SigmaHPBar : MonoBehaviour
{
    public Image SigmaHP;

    SigmaBehaviour sigmaBv;

    void Start()
    {
    }

    void Update()
    {
        if(RockManGameManager.Instance.IsPlaying())
        {
            if(sigmaBv == null)
            {
                sigmaBv = GameObject.Find( "SigmaHead" ).GetComponent<SigmaBehaviour>();
            }
            else
            {
                SigmaHP.fillAmount = sigmaBv.curHP / sigmaBv.maxHP;
            }
        }
    }


}
