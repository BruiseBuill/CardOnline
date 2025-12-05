using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline
{
    [CreateAssetMenu(fileName = "FightActive", menuName = "CardOnline/Input/FightActive")]
    public class IM_FightActive : Input2ActMode
    {
        [SerializeField] GenericEventChannel<Vector3> ch_OnNormalPressDown;
        [SerializeField] GenericEventChannel<Vector3> ch_OnNormalPressUp;
        [SerializeField] GenericEventChannel<Vector3,Vector3> ch_OnNormalDrag;

        [SerializeField] GenericEventChannel<Vector3> ch_Accelerate;

        public override void SetActMode()
        {
            InputManager.onPointerDown += (screenPos) => ch_OnNormalPressDown.Invoke(screenPos);
            InputManager.onPointerUp += (screenPos)=> ch_OnNormalPressUp.Invoke(screenPos);
            InputManager.onDrag += (start, end) => ch_OnNormalDrag.Invoke(start, end);
            ch_Accelerate.AddListener(Accelerate);
        }

        public override void UnSetActMode()
        {
            InputManager.onPointerDown = delegate { };
            InputManager.onPointerUp = delegate { };
            InputManager.onDrag = delegate { };
            ch_Accelerate.RemoveListener(Accelerate);
        }

        public override void Update()
        {
            
        }
        void Accelerate(Vector3 screenPos) 
        {
            CursorManager.Instance().ChangeAct(1);
        }
    }
}