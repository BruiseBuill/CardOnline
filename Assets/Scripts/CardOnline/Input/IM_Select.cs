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

        public override void SetActMode()
        {
            InputManager.onPointerDown
            InputManager.onClick += (screenPos) => ch_OnSelectClick.Invoke(screenPos);
        }
        public override void UnSetActMode()
        {
            InputManager.onClick = delegate { };
        }
        public override void Update()
        {

        }
    }
}