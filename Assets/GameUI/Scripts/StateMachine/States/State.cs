using System.Collections;
using System;
namespace Q4u
{
    /// <summary>
    /// A generic empty state. Pass onExecute action into Constructor to run once when entering the state
    /// (or null to do nothing)
    /// </summary>
    public class State : AbstractState
    {
        readonly Action m_OnExecute;

        /// <param name="onExecute">An event that is invoked when the state is executed</param>
        ///
        // Constructor takes delegate to execute and optional name (for debugging)
        public State(Action onExecute, string stateName = nameof(State), bool enableDebug = false)
        {
            m_OnExecute = onExecute;
            Name = stateName;

            //Log the state change in the console
            DebugEnable = enableDebug;
        }

        public override IEnumerator Execute()
        {
            yield return null;
            if (m_Debug)
                base.LogCurrentState();

            //Invokes the m_OnExecute Action if it exits
            m_OnExecute?.Invoke();
        }
    }
}
