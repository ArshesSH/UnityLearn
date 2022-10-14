using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SPD_End : MonoBehaviour
{
    private void OnGUI()
    {
        if (GUI.Button(new Rect(1210.0f, 400.0f, 150.0f, 30.0f), "Go to Start Screen"))
        {
            SPDGameManager.Instance.ResetGame();
            SPDGameManager.Instance.ChangeScene("08_0 SpartaStart");
        }
    }
}
