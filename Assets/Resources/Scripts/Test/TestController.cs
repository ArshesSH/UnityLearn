using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KSH_Lib;

public class TestController : MonoBehaviour
{
    #region Public Fields
    #endregion


    #region Private Fields

    Behavior<TestController> behavior;

    #endregion


    #region MonoBehaviour Callbacks
    private void Start()
    {
        behavior = new TestBvA();
    }
    private void Update()
    {
        print( behavior.Count );
        PushStateToA();
        PushStateToB();

        behavior.Update( this, ref behavior );
    }
    #endregion

    #region Private Methods
    void PushStateToA()
    {
        if( Input.GetKeyDown( KeyCode.A ) )
        {
            behavior.PushSuccessorState( new TestBvA() );
        }
    }
    void PushStateToB()
    {
        if ( Input.GetKeyDown( KeyCode.B ) )
        {
            behavior.PushSuccessorState( new TestBvB() );
        }
    }

    #endregion


    #region Behaviors
    public class TestBvA : Behavior<TestController>
    {
        public override void Activate( in TestController actor )
        {
            Debug.Log( "Behavior A Start!" );
        }
        public override Behavior<TestController> DoBehavior( in TestController actor )
        {
            if ( actor.behavior.HasSuccessors() )
            {
                return PassState();
            }
            Debug.Log( "Do Behavior A" );
            return null;
        }
    }
    public class TestBvB : Behavior<TestController>
    {
        public override void Activate( in TestController actor )
        {
            Debug.Log( "Behavior B Start!" );
        }
        public override Behavior<TestController> DoBehavior( in TestController actor )
        {
            if(actor.behavior.HasSuccessors())
            {
                return PassState();
            }
            Debug.Log( "Behavior B" );
            return null;
        }
    }

    #endregion
}
