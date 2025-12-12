using BF;
using BF.Utility;
using CardOnline.Card;
using CardOnline.Character;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.Character
{
	public class PlayerInput : CharacterInput
	{
        public HoverCard hoverCard;
        [ReadOnly]
        public MagicCard selectedCard;
        [ReadOnly]
        public MagicCard scanCard;

        public bool isSelected = false; 
        public bool hasTarget = false; 

        Vector3 hoverCardPos;
        Camera camera;

        CharacterData characterData;
        [SerializeField] CardAllignment cardAllignment;
        [SerializeField] CardLogicSystem cardController;

        [SerializeField] float duration = 0.5f;

        [Header("In_Event")]
        [SerializeField] GenericEventChannel<object> ch_onScanCard;
        [SerializeField] EventChannel ch_onScanCardEnd;
        [SerializeField] GenericEventChannel<object> ch_onPullCard;
        [SerializeField] EventChannel ch_onPullCardEnd;
        [SerializeField] GenericEventChannel<Vector3, Vector3> ch_onDragCard;


        [Header("Out_Event")]
        [SerializeField] GenericEventChannel<bool> onCloseRaycast;
        [SerializeField] GenericEventChannel<MagicCard> onSelectCard;


        void Start()
        {
            camera = Camera.main;
            hoverCard.Hide();
        }
        void OnDestroy()
        {
            hoverCard.Hide();
        }

        public override void Open()
        {
            ch_onScanCard.AddListener(OnScanCard);
            ch_onPullCard.AddListener(OnPullCard);
            ch_onScanCardEnd.AddListener(OnScanCardEnd);
            ch_onPullCardEnd.AddListener(OnPullCardEnd);
            ch_onDragCard.AddListener(OnDragCard);
        }
        public override void Close()
        {
            ch_onScanCard.RemoveListener(OnScanCard);
            ch_onPullCard.RemoveListener(OnPullCard);
            ch_onScanCardEnd.RemoveListener(OnScanCardEnd);
            ch_onPullCardEnd.RemoveListener(OnPullCardEnd);
            ch_onDragCard.RemoveListener(OnDragCard);
        }
        
        void OnScanCard(object c)
        {
            MagicCard card = c as MagicCard;
            if (scanCard == card)
            {
                return;
            }
            scanCard = card;
            hoverCardPos = hoverCardPos.ResetXY(card.transform.position);
            hoverCard.Show(card, hoverCardPos);
        }
        void OnPullCard(object c)
        {
            MagicCard card = c as MagicCard;

            onCloseRaycast.Invoke(true);

            selectedCard = card;
            card.Hide();

            //float y = cardAllignment.GetCentralControlPos().y;
            //hoverCardPos = new Vector3(card.transform.position.x, y, -1f);
            hoverCardPos = hoverCardPos.ResetXY(scanCard.transform.position);
            hoverCard.Show(card, hoverCardPos);
        }


        void OnScanCardEnd()
        {
            hoverCard.Hide();
            scanCard = null;
        }
        void OnPullCardEnd()
        {
            hoverCard.Hide();

            selectedCard.transform.position = selectedCard.transform.position.ResetXY(hoverCardPos);
            selectedCard.transform.rotation = Quaternion.identity;
            selectedCard.Show();

            cardAllignment.UpdateCardPositions();

            selectedCard = null;
            isSelected = false;
            
            onCloseRaycast.Invoke(false);
        }
        void OnDragCard(Vector3 start, Vector3 end)
        {
            Vector3 offset = camera.ScreenToWorldPoint(end) - camera.ScreenToWorldPoint(start);
            hoverCard.ChangePosition(hoverCardPos + new Vector3(offset.x, offset.y, 0));
        }

    }
}