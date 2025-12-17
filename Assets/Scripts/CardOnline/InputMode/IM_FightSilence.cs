using BF;
using CardOnline.Card;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CardOnline
{
    [CreateAssetMenu(fileName = "FightSilence", menuName = "CardOnline/Input/FightSilence")]
    public class IM_FightSilence : Input2ActMode
    {
        [SerializeField] GenericEventChannel<object> ch_onScanCard;
        [SerializeField] EventChannel ch_onScanCardEnd;

        MagicCard scanCard;

        public override void SetActMode()
        {
            InputManager.onPointerDown += OnPointDown;
            InputManager.onPointerUp += OnPointUp;
            InputManager.onDrag += OnDrag;
        }
        public override void UnSetActMode()
        {
            InputManager.onPointerDown = delegate { };
            InputManager.onPointerUp = delegate { };
            InputManager.onDrag = delegate { };
        }
        void OnPointDown(Vector3 screenPos)
        {
            var card = PhysicsRaycast.Instance().RaycastCard(screenPos);
            if (card != null)
            {
                ch_onScanCard.Invoke(card);
            }
        }
        void OnPointUp(Vector3 screenPos)
        {
            if (scanCard != null)
            {
                scanCard = null;
                ch_onScanCardEnd.Invoke();
            }
        }
        void OnDrag(Vector3 start, Vector3 end)
        {
            var card = PhysicsRaycast.Instance().RaycastCard(end);
            if (card != null)
            {
                scanCard = card;
                ch_onScanCard.Invoke(card);
            }
            else
            {
                scanCard = null;
                ch_onScanCardEnd.Invoke();
            }
        }
        public override void Update()
        {
            
        }
    }
}
