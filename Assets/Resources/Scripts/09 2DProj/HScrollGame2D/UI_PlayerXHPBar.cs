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
        xController = RockManGameManager.Instance.XController;
    }

    void Update()
    {
        PlayerXHP.fillAmount = xController.curHP / xController.maxHP;
    }

}
