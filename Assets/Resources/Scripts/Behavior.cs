using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSH_Lib
{
    abstract class Behavior
    {
        #region Public Fields
        #endregion


        #region Private Fields
        List<Behavior> stateStack;
        #endregion


        #region Public Methods

        public abstract Behavior Update();
        public virtual void Activate() { }
        public void SetSuccessorStates(in List<Behavior> successors)
        {
            stateStack = successors;
        }
        public void PushSuccessorState(in Behavior behavior)
        {
            stateStack.Add( behavior );
        }
        public void PushSuccessorStates( in List<Behavior> behaviors )
        {
            stateStack.AddRange( behaviors );
        }
        public bool HasSuccessors()
        {
            return stateStack.Count != 0;
        }
        //public Behavior PassState()
        //{
          
        //}

        #endregion


        #region Private Methods
        #endregion
    }
}