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
        GUI.Box(new Rect(600.0f, 400.0f, 180.0f, 220.0f), "\n���� ����\n\nW,A,S,D: �̵�\nAlt: �ȱ�\nLShift: �ٱ�\n���콺 Ŭ�� or LCtrl: ����\n���콺 ��: Ȯ��\nF: ���� ����");
    }
}
