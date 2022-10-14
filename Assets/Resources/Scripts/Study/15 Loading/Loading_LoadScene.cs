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
        async = SceneManager.LoadSceneAsync( sceneName );  // 별도의 명령이 있을 때 까지 scene이 넘어가지 않음
        async.allowSceneActivation = false;
        
        while(async.progress < 0.9f) //async.progress 0~1 사이의 값
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

        async.allowSceneActivation = true; // 이게 true여야 씬 변경 됨
        yield return true;
    }
    #endregion
}
