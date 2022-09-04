using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RockmanHealthBar : MonoBehaviour
{
    public Image PlayerXHP;
    public Image SigmaHP;

    SigmaBehaviour sigmaBv;
    PlayerXController xController;

    void Start()
    {
        sigmaBv = RockManGameManager.Instance.SigmaBv;
        xController = RockManGameManager.Instance.XController;
    }

    void Update()
    {
        PlayerXHP.fillAmount = xController.curHP / xController.maxHP;
        SigmaHP.fillAmount = sigmaBv.curHP / sigmaBv.maxHP;
    }

}
