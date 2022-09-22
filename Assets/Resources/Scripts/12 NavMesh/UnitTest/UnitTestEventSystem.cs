using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 이벤트 등록
using UnityEngine.Events;

public class UnitTestEventSystem : MonoBehaviour
{
    //1. 이벤트 등록
    UnityEvent myEvent;

    private void Start()
    {
        if(myEvent == null )
        {
            myEvent = new UnityEvent();
        }
        // 등록
        myEvent.AddListener( Event1 );
        myEvent.AddListener( Event2 );
        myEvent.AddListener( Event1 );
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            // 호출 시 등록된 모든 이벤트가 순차적으로 실행됨
            myEvent.Invoke();
        }
    }

    void Event1()
    {
        Debug.Log( "Event 1 called" );
    }

    void Event2()
    {
        Debug.Log( "Event 2 called" );
    }

}
