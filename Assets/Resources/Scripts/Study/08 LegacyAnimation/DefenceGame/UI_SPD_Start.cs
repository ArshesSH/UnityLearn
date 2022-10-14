using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SPD_Start : MonoBehaviour
{
    private void OnGUI()
    {
        if (GUI.Button(new Rect(810.0f, 400.0f, 150.0f, 30.0f), "Game Start"))
        {
            SPDGameManager.Instance.ChangeScene("08_1 SpartaMain");
        }
        GUI.Box(new Rect(600.0f, 400.0f, 180.0f, 220.0f), "\n조작 설명\n\nW,A,S,D: 이동\nAlt: 걷기\nLShift: 뛰기\n마우스 클릭 or LCtrl: 공격\n마우스 휠: 확대\nF: 시점 변경");
    }
}
