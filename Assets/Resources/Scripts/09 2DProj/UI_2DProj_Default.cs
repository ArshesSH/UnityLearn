using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_2DProj_Default : MonoBehaviour
{
    public GameObject textObj = null;
    void Start()
    {
        textObj.GetComponent<Text>().text = "��";
    }
    void Update()
    {
        
    }
}
