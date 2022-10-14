using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_2DProj_Option : MonoBehaviour
{
    Text titleText;
    public InputField inputField;

    void Start()
    {
        titleText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        
    }

    void onClickOK()
    {
        Debug.Log( "onClickOK" );
        gameObject.SetActive( false );
    }
    void onTextChanged()
    {
        if(titleText != null)
        {
            titleText.text = inputField.text;
        }
    }
    void onTextEndEdit()
    {
        if ( titleText != null )
        {
            titleText.text = inputField.text;
        }
    }

}
