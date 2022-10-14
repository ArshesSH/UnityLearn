using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerXHPBar : MonoBehaviour
{
    public Image PlayerXHP;

    PlayerXController xController;

    void Start()
    {
    }

    void Update()
    {
        if( xController == null)
        {
            xController = RockManGameManager.Instance.XController;
        }
        else
        {
            PlayerXHP.fillAmount = xController.curHP / xController.maxHP;
        }

    }

}
