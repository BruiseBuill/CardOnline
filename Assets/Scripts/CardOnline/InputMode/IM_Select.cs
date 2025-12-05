using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline
{
    [CreateAssetMenu(fileName = "Select", menuName = "CardOnline/Input/Select")]
    public class IM_Select : Input2ActMode
    {
        [SerializeField] GenericEventChannel<Vector3> ch_OnSelectPressDown;
        [SerializeField] GenericEventChannel<Vector3> ch_OnSelectClick;

        [SerializeField] EventChannel ch_EndAccelerate;

        public override void SetActMode()
        {
            InputManager.onPointerDown += (screenPos) => ch_OnSelectPressDown.Invoke(screenPos);
            InputManager.onClick += (screenPos) => ch_OnSelectClick.Invoke(screenPos);
            ch_EndAccelerate.AddListener(EndAccelerate);
        }
        public override void UnSetActMode()
        {
            InputManager.onPointerDown =delegate { };
            InputManager.onClick = delegate { };
            ch_EndAccelerate.RemoveListener(EndAccelerate);
        }
        public override void Update()
        {

        }
        void EndAccelerate() 
        {
            CursorManager.Instance().ChangeAct(0);
        }
    }
}