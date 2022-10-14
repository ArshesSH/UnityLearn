using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstacleObj = null;

   // Spawner Case 1 IEnumerator
     // IEnumerator�� �ϸ� �ڷ�ƾ ó�� �����ϰ� ��
    IEnumerator Start()
    {
        while ( true )
        {
            if ( GameManager_FlappyBird.Instance.IsGamePlaying() )
            {
                GameObject obj = null;
                if ( obstacleObj != null )
                {
                    // �θ� ������Ʈ ���̱�
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
        yield return new WaitForSeconds( delayTime );   // �־����
        //�ƹ��͵� ���Ϸ��� yield return null;
        // Ư�� ���ǿ��� �ڱ� �ڽ��� �ڷ�ƾ�� �������� yield break;
        // �ڷ�ƾ ���ο� yield�� ������ �� �� ����, �̷� ��� 
        count++;
        print( count );
        StartCoroutine( "CountTime", 1.0f );    //while�� �����ص� ��
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
