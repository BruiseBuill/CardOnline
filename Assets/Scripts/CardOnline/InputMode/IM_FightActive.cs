using BF;
using CardOnline.Card;
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




        //
        [SerializeField] GenericEventChannel<> ch_onScanCard;
        [SerializeField]
        [SerializeField] LayerMask cardLayer;

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
        void OnPointDown(Vector3 screenPos)
        {
            var card = RaycastCard(screenPos);
            if (card != null)
            {

            }

        }
        MagicCard RaycastCard(Vector3 screenPos)
        {
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(screenPos), Vector3.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, 1 << cardLayer)) 
            {
                Collider collider = hitInfo.collider;
                MagicCard card = collider.GetComponentInParent<MagicCard>();
                return card;
            }
            else
            {
                return null;
            }
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