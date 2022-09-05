using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitleScreen : MonoBehaviour
{
    public enum TitleState
    {
        FadeIn,
        Wait,
        FadeOut,
        Load
    }

    public InputField inputField;
    public GameObject nullMessage;
    public GameObject titleImageObj;
    public GameObject titleUI;


    public float FadeInDelay = 0.01f;
    public float FadeOutDelay = 0.005f;

    Image titleImg;
    float titleTimer = 0.0f;
    byte colorCoef = 0;

    TitleState titleState = TitleState.FadeIn;
    bool isUIShowed = false;

    void Start()
    {
        titleImg = titleImageObj.GetComponent<Image>();
        RockManGameManager.Instance.PlayBGM( 0 );
    }

    void Update()
    {
        switch (titleState)
        {
            case TitleState.FadeIn:
            {
                FadeIn();
            }
            break;

            case TitleState.Wait:
            {
                ShowUI();
            }
            break;
            case TitleState.FadeOut:
            {
                FadeOut();
            }
            break;
            case TitleState.Load:
            {
                RockManGameManager.Instance.ChangeScene( "09_1 RockmanMain" );
            }
            break;  
        }
       
    }

    void FadeIn()
    {
        if ( colorCoef < 255 )
        {
            titleTimer += Time.deltaTime;

            if ( titleTimer >= FadeInDelay )
            {
                if ( titleImg )
                {
                    titleImg.color = new Color32( colorCoef, colorCoef, colorCoef, 255 );
                    colorCoef++;
                }
                titleTimer = 0.0f;
            }
        }
        else
        {
            titleState = TitleState.Wait;
            titleTimer = 0.0f;
        }
    }

    void FadeOut()
    {
        if ( colorCoef > 0 )
        {
            titleTimer += Time.deltaTime;

            if ( titleTimer >= FadeOutDelay )
            {
                if ( titleImg )
                {
                    titleImg.color = new Color32( colorCoef, colorCoef, colorCoef, 255 );
                    colorCoef--;
                }
                titleTimer = 0.0f;
            }
        }
        else
        {
            titleState = TitleState.Load;
            titleTimer = 0.0f;
        }
    }


    void ShowUI()
    {
        if ( !isUIShowed )
        {
            titleUI.SetActive( true );
            isUIShowed = true;
        }
    }


    void onClickOK()
    {
        if (RockManGameManager.Instance.userID == null)
        {
            nullMessage.SetActive(true);
        }
        else
        {
            titleUI.SetActive( false );
            titleState = TitleState.FadeOut;
            RockManGameManager.Instance.PlaySFX( 1 );
        }
    }




    void onTextChanged()
    {
        RockManGameManager.Instance.userID = inputField.text.ToString();
        RockManGameManager.Instance.PlaySFX( 0 );
    }
    void onTextEndEdit()
    {
        RockManGameManager.Instance.userID = inputField.text.ToString();
    }

}
