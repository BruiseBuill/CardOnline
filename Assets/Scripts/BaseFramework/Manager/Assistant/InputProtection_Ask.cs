using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BF
{
    [Serializable]
    public class InputProtection_Ask
    {
        //The necessary condition for this action
        //Normally, action can only be done when condition=true and input
        //Advance: input before condition=true
        public Action onAct = delegate { };
        [ReadOnly][SerializeField] bool hasInput;
        [ReadOnly][SerializeField] float lastInputTime;

        [SerializeField] float advanceProtectionTime;

        public InputProtection_Ask()
        {
            hasInput = false;
            lastInputTime = 0f;
        }
        public void Input()
        {
            hasInput = true;
            lastInputTime = Time.time;
        }
        public void Update()
        {
            if (hasInput && Time.time - lastInputTime < advanceProtectionTime) 
            {
                onAct.Invoke();
            }
        }
        public void OnActOver(bool isSuccess)
        {
            if (isSuccess)
            {
                hasInput = false;
            }
        }
    }

}
