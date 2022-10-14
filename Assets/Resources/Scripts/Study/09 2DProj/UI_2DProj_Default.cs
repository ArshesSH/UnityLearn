using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_2DProj_Default : MonoBehaviour
{
    public GameObject textObj = null;
    public Image imgHPBar;
    int hpMax = 100;

    void Start()
    {
        textObj.GetComponent<Text>().text = "”î";
        ShowHPBar( 50 );
    }
    void Update()
    {
        
    }

    public void ShowHPBar(int hp)
    {
        imgHPBar.fillAmount = (float)hp / (float)hpMax;
    }
}
