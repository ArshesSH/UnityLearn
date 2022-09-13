using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeStartUI : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnStartButtonClick()
    {
        GameManager_MazeRunner.Instance.ChangeScene("MazeRunner");
    }
}
