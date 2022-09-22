using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTestDelegate : MonoBehaviour
{
    delegate int Calculate( int lhs, int rhs );

    Calculate delegateCalc = null;

    delegate void FuncHandler();
    FuncHandler funcHandler;


    void Start()
    {
        delegateCalc = new Calculate( AddNum );

        // √º¿Œ
        funcHandler = new FuncHandler( Func1 );
        funcHandler += new FuncHandler( Func2 );
        funcHandler += new FuncHandler( Func3 );
        funcHandler += new FuncHandler( Func1 );
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            delegateCalc = new Calculate( AddNum );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha2 ) )
        {
            delegateCalc = new Calculate( SubNum );
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log( delegateCalc( 11, 33 ) );
        }
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            funcHandler();
        }
    }

    int AddNum(int lhs, int rhs)
    {
        return lhs + rhs;
    }
    int SubNum(int lhs, int rhs)
    {
        return lhs - rhs;
    }

    void Func1()
    {
        Debug.Log( "Func1" );
    }
    void Func2()
    {
        Debug.Log( "Func2" );
    }
    void Func3()
    {
        Debug.Log( "Func3" );
    }

}
