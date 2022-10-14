using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MazeGameUI : MonoBehaviour
{
    
    public TextMeshProUGUI gameStateText;
    public GameObject PanelObj;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager_MazeRunner.Instance._MazeManager.IsVictory())
        {
            gameStateText.text = "Victory";
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
            PanelObj.SetActive(true);
        }
        if (GameManager_MazeRunner.Instance._MazeManager.IsDead())
        {
            gameStateText.text = "You Died";
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
            PanelObj.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        GameManager_MazeRunner.Instance.ChangeScene("MazeRunnerStartScene");
    }
}
