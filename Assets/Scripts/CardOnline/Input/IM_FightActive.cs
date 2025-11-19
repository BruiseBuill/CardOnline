using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline
{
    [CreateAssetMenu(fileName = "FightActive", menuName = "CardOnline/Input/FightActive")]
    public class IM_FightActive : Input2ActMode
    {
        [SerializeField] GenericEventChannel<Vector3> ch_OnSelect;
        [SerializeField] GenericEventChannel<Vector3> ch_OnUnselect;
        [SerializeField] GenericEventChannel<Vector3,Vector3> ch_OnDrag;

        public override void SetActMode()
        {
            InputManager.onPointerDown += (screenPos) => ch_OnSelect.Invoke(screenPos);
            InputManager.onPointerUp += (screenPos)=> ch_OnUnselect.Invoke(screenPos);
            InputManager.onDrag += (start, end) => ch_OnDrag.Invoke(start, end);
        }

        public override void UnSetActMode()
        {
            InputManager.onPointerDown = delegate { };
            InputManager.onPointerUp = delegate { };
            InputManager.onDrag = delegate { };
        }

        public override void Update()
        {
            
        }
    }
}