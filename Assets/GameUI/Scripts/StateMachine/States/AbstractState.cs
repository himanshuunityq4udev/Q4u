using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Q4u
{
    /// <summary>
    /// An abstract class that provides common functionalities for the states of state machines
    /// </summary>
    public abstract class AbstractState :IState
    { 
        /// <summary>
        /// The name of the state used for debugging purposes
        /// </summary>
        public virtual string Name { get; set; }

        // Enable debug messages
        protected bool m_Debug = false;
        readonly List<ILink> m_Links = new();

        public bool DebugEnable { get => m_Debug; set => m_Debug = value; }

        public virtual void Enter()
        {

        }

        public abstract IEnumerator Execute();

        public virtual void Exit()
        {

        }

        public virtual void AddLink(ILink link)
        {
            if (!m_Links.Contains(link))
            {
                m_Links.Add(link);
            }
        }

        public virtual void RemoveLink(ILink link)
        {
            if (m_Links.Contains(link))
            {
                m_Links.Remove(link);
            }
        }

        public virtual void RemoveAllLinks()
        {
            m_Links.Clear();
        }

        public virtual bool ValidateLinks(out IState nextState)
        {
            if(m_Links!=null && m_Links.Count > 0)
            {
                foreach (var link in m_Links)
                {
                    var result = link.Validate(out nextState);
                    if (result)
                    {
                        return true;
                    }
                }
            }
            //By default, return false without a valid Istate
            nextState = null;
            return false;
        }

        public void EnableLinks()
        {
            foreach (var link in m_Links)
            {
                link.Enable();
            }
        }

        public void DisableLinks()
        {
            foreach (var link in m_Links)
            {
                link.Disable();
            }
        }

        public virtual void LogCurrentState()
        {
            if (m_Debug)
                Debug.Log("Current state = " + Name + "(" + this.GetType().Name + ")");
        }
    }
}