using BF;
using CardOnline.Card;
using CardOnline.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline
{
    public enum IM_Index { FightActive, FightSilence,Select }

    [CreateAssetMenu(fileName = "FightActive", menuName = "CardOnline/Input/FightActive")]
    public class IM_FightActive : Input2ActMode
    {
        [SerializeField] GenericEventChannel<Vector3> ch_Accelerate;
        //
        [SerializeField] GenericEventChannel<object> ch_onScanCard;
        [SerializeField] EventChannel ch_onScanCardEnd;
        [SerializeField] GenericEventChannel<object> ch_onPullCard;
        [SerializeField] EventChannel ch_onPullCardEnd;
        [SerializeField] GenericEventChannel<Vector3,Vector3> ch_onDragCard;

        //
        [SerializeField] bool isInScan;
        [SerializeField] bool isPullingCard;

        [SerializeField] LayerMask cardLayer;

        public override void SetActMode()
        {
            InputManager.onPointerDown += OnPointDown;
            InputManager.onPointerUp += OnPointUp;
            InputManager.onDrag += OnDrag;


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
            isInScan = false;
            isPullingCard = false;

            var card = PhysicsRaycast.Instance().RaycastCard(screenPos);
            if (card == null)
            {
                isInScan = true;
                return;
            }
            if (card.isInCoolDown) 
            {
                isInScan = true;
                ch_onScanCard.Invoke(card);
                return;
            }
            if (!card.characterControl.Data.isPlayer)
            {
                isInScan = true;
                return;
            }
            isPullingCard = true;
            ch_onPullCard.Invoke(card);
        }
        void OnPointUp(Vector3 screenPos)
        {
            if (isInScan)
            {
                isInScan = false;
                ch_onScanCardEnd.Invoke();
            }
            if (isPullingCard)
            {
                isPullingCard = false;
                ch_onPullCardEnd.Invoke();
            }
        }
        void OnDrag(Vector3 start,Vector3 end)
        {
            if (isInScan)
            {
                var card = PhysicsRaycast.Instance().RaycastCard(end);
                if (card != null)
                {
                    ch_onScanCard.Invoke(card);
                }
                else
                {
                    ch_onScanCardEnd.Invoke();
                }
            }
            if (isPullingCard)
            {
                ch_onDragCard.Invoke(start, end);
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