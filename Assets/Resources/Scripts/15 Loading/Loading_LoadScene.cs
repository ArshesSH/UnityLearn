using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading_LoadScene : MonoBehaviour
{
    #region Public Fields
    public Slider slider;
    #endregion


    #region Private Fields
    AsyncOperation async;
    float delayTimer;
    #endregion


    #region MonoBehaviour Callbacks
    private void Start()
    {
        StartCoroutine( LoadingNextScene( GameManager.Instance.nextSceneName ) );
    }
    private void Update()
    {
        DelayTime();
    }
    #endregion


    #region Public Methods
    #endregion


    #region Private Methods
    void DelayTime()
    {
        delayTimer += Time.deltaTime;
    }

    #endregion

    #region IEnumerators
    IEnumerator LoadingNextScene(string sceneName)
    {
        async = SceneManager.LoadSceneAsync( sceneName );  // ������ ����� ���� �� ���� scene�� �Ѿ�� ����
        async.allowSceneActivation = false;
        
        while(async.progress < 0.9f) //async.progress 0~1 ������ ��
        {
            slider.value = async.progress;
            yield return true;
        }

        while(async.progress >= 0.9f)
        {
            slider.value = async.progress;
            yield return new WaitForSeconds( 0.1f );
            if ( delayTimer >= 2.0f )
            {
                break;
            }
        }

        async.allowSceneActivation = true; // �̰� true���� �� ���� ��
        yield return true;
    }
    #endregion
}
