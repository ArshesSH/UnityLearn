using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. �̺�Ʈ ���
using UnityEngine.Events;

public class UnitTestEventSystem : MonoBehaviour
{
    //1. �̺�Ʈ ���
    UnityEvent myEvent;

    private void Start()
    {
        if(myEvent == null )
        {
            myEvent = new UnityEvent();
        }
        // ���
        myEvent.AddListener( Event1 );
        myEvent.AddListener( Event2 );
        myEvent.AddListener( Event1 );
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            // ȣ�� �� ��ϵ� ��� �̺�Ʈ�� ���������� �����
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
