using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RacingGame_Start : MonoBehaviour
{
    private void OnGUI()
    {
        if (GUI.Button(new Rect(400.0f, 30.0f, 150.0f, 30.0f), "Game Start"))
        {
            GameManager.Instance.ChangeScene("01-1 Race");
        }
        GUI.Box(new Rect(400.0f, 60.0f, 150.0f, 120.0f), "\n���� ����\n\nW: ����\nS: �극��ũ, ����\nA, D: �¿� ����");
    }
}
