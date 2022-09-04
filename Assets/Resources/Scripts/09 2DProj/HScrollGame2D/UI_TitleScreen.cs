using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitleScreen : MonoBehaviour
{
    public InputField inputField;
    public GameObject nullMessage;


    void Start()
    {
    }

    void Update()
    {

    }
    void onClickOK()
    {
        if (RockManGameManager.Instance.userID == null)
        {
            nullMessage.SetActive(true);
        }
        else
        { 
            RockManGameManager.Instance.ChangeScene("09_1 RockmanMain");
        }
    }
    void onTextChanged()
    {
        RockManGameManager.Instance.userID = inputField.text.ToString();
    }
    void onTextEndEdit()
    {
        RockManGameManager.Instance.userID = inputField.text.ToString();
    }

}
