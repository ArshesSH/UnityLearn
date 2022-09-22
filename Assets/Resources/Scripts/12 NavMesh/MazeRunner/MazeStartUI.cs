using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeStartUI : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        
    }

    public void OnStartButtonClick()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager_MazeRunner.Instance.ChangeScene("MazeRunner");
    }
    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
