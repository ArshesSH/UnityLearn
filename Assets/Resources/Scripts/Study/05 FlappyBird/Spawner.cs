using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstacleObj = null;

   // Spawner Case 1 IEnumerator
     // IEnumerator로 하면 코루틴 처럼 동작하게 됨
    IEnumerator Start()
    {
        while ( true )
        {
            if ( GameManager_FlappyBird.Instance.IsGamePlaying() )
            {
                GameObject obj = null;
                if ( obstacleObj != null )
                {
                    // 부모에 오브젝트 붙이기
                    obj = Instantiate( obstacleObj, transform.position, transform.rotation );
                }
            }

            yield return new WaitForSeconds( 1.5f );
        }
    }

    //Spawner Case2 Invoke
    /*
    private void Start()
    {
        Invoke( "MakeObj", 1.0f );
    }

    void MakeObj()
    {
        GameObject obj = null;
        if ( obstacleObj != null )
        {
            obj = Instantiate( obstacleObj, transform.position, transform.rotation );
            Invoke( "MakeObj", 1.0f );
        }
    }
    */


    //Spawner Case3 InvokeRepeating
    //private void Start()
    //{
    //    InvokeRepeating( "MakeObj", 1.0f, 1.0f );
    //}

    //void MakeObj()
    //{
    //    GameObject obj = null;
    //    if ( obstacleObj != null )
    //    {
    //        obj = Instantiate( obstacleObj, transform.position, transform.rotation );
    //    }
    //}


    // Case 4 Coroutine
    /*
    IEnumerator enumerator;
    private void Start()
    {
        StartCoroutine( "CountTime", 1 );
    }

    private int count = 0;

    IEnumerator CountTime(float delayTime)
    {
        yield return new WaitForSeconds( delayTime );   // 있어야함
        //아무것도 안하려면 yield return null;
        // 특정 조건에서 자기 자신의 코루틴을 끝내려면 yield break;
        // 코루틴 내부에 yield가 여러번 들어갈 수 있음, 이런 경우 
        count++;
        print( count );
        StartCoroutine( "CountTime", 1.0f );    //while로 루프해도 됨
    }

    private void Update()
    {
        if(count >10)
        {
            StopCoroutine( "CountTime" );
        }
    }
    */

    //or 

    //IEnumerator enumerator;
    //private void Start()
    //{
    //    enumerator = CountTime( 1.0f );
    //    StartCoroutine( enumerator );
    //}

    //private int count = 0;

    //IEnumerator CountTime( float delayTime )
    //{
    //    yield return new WaitForSeconds( delayTime );
    //    count++;
    //    print( count );
    //}



}
