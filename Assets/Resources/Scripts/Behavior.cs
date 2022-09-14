using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace KSH_Lib
{
    public class Behavior<T>
    {
        #region Public Fields
        public int Count
        {
            get
            {
                return stateStack.Count();
            }
        }

        #endregion

        #region Private Fields

        List<Behavior<T>> stateStack = new List<Behavior<T>>();

        #endregion


        #region Public Methods
            
        public virtual void Update( in T actor, ref Behavior<T> state )
        {
            Behavior<T> newState = DoBehavior( actor );
            if ( newState != null )
            {
                state = newState;
                state.Activate( actor );    
            }
        }

        public virtual void Activate( in T actor ) { }
        public virtual Behavior<T> DoBehavior( in T actor ) { return null; }
        public void SetSuccessorStates(in List<Behavior<T>> successors)
        {
            stateStack = successors;
        }
        public void PushSuccessorState(in Behavior<T> behavior )
        {
            stateStack.Add( behavior );
        }
        public void PushSuccessorStates( in List<Behavior<T>> behaviors )
        {
            stateStack.AddRange( behaviors );
        }
        public bool HasSuccessors()
        {
            return stateStack.Count != 0;
        }
        public Behavior<T> PassState()
        {
            var ps = stateStack.Last();
            stateStack.RemoveAt( stateStack.Count - 1 );
            ps.SetSuccessorStates( stateStack );
            return ps;
        }

        #endregion
    }
}