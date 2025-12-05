using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardOnline.Card;
using Sirenix.OdinInspector;
using CardOnline.Character;

namespace CardOnline.Character
{
	public class PlayerInput : CharacterInput
	{
        public HoverCard hoverCard;
        [ReadOnly]
        public MagicCard selectedCard;
        [ReadOnly]
        public MagicCard observedCard;

        public bool isSelected = false; 
        public bool hasTarget = false; 

        Vector3 hoverCardPos;
        Camera camera;

        CharacterData characterData;
        [SerializeField] CardAllignment cardAllignment;
        [SerializeField] CharacterCardSystem cardController;

        [SerializeField] float duration = 0.5f;

        [Header("In_Event")]
        [SerializeField] GenericEventChannel<Vector3> ch_OnNormalPressDown;
        [SerializeField] GenericEventChannel<Vector3> ch_OnNormalPressUp;
        [SerializeField] GenericEventChannel<Vector3, Vector3> ch_OnNormalDrag;
        [SerializeField] GenericEventChannel<Vector3> ch_OnSelectClick;
        

        [Header("Out_Event")]
        [SerializeField] GenericEventChannel<bool> onCloseRaycast;
        [SerializeField] GenericEventChannel<MagicCard> onSelectCard;


        void Start()
        {
            camera = Camera.main;
            hoverCard.Hide();

            ch_OnNormalPressDown.AddListener(NormalPressDown);
            ch_OnNormalPressUp.AddListener(NormalPressUp);
            ch_OnNormalDrag.AddListener(OnNormalDrag);
            ch_OnSelectClick.AddListener(SelectClick);
        }
        void OnDestroy()
        {
            hoverCard.Hide();
            ch_OnNormalPressDown.RemoveListener(NormalPressDown);
            ch_OnNormalPressUp.RemoveListener(NormalPressUp);
            ch_OnNormalDrag.RemoveListener(OnNormalDrag);
            ch_OnSelectClick.RemoveListener(SelectClick);
        }
        void NormalPressDown(Vector3 screenPos)
        {
            MagicCard card = RaycastCard(screenPos);
            if (card == null)
            {
                isSelected = false;
                return;
            }

            if (card.isInCoolDown)
            {
                isSelected = false;
                observedCard = card;
                hoverCardPos = observedCard.transform.position;
                hoverCard.Show(card, hoverCardPos);
            }
            onCloseRaycast.Invoke(true);

            isSelected = true;
            selectedCard = card;

            card.Hide();

            float y = cardAllignment.GetCentralControlPos().y;
            hoverCardPos = new Vector3(card.transform.position.x, y, -1f);
            hoverCard.Show(card, hoverCardPos);
        }
        void NormalPressUp(Vector3 screenPos)
        {
            if (isSelected)
            {
                SelectUnSelect(screenPos);
            }
            else
            {
                ObserveUnSelect();
            }
        }
        void SelectUnSelect(Vector3 screenPos)
        {
            selectedCard.transform.position = hoverCard.transform.position;
            selectedCard.transform.rotation = Quaternion.identity;
            selectedCard.Show();
            cardAllignment.UpdateCardPositions();

            if (UseCardCheck(screenPos))
            {
                UseCard();
            }
            isSelected = false;
            selectedCard = null;
            onCloseRaycast.Invoke(false);
            hoverCard.Hide();
        }
        void ObserveUnSelect()
        {
            hoverCard.Hide();
            if (observedCard != null)
                observedCard = null;
        }
        #region Drag
        void OnNormalDrag(Vector3 start, Vector3 end)
        {
            if (isSelected)
            {
                MoveHoverCard(start, end);
            }
            else
            {
                ObserveCard(start, end);
            }
        }
        void ObserveCard(Vector3 start, Vector3 end)
        {
            MagicCard card = RaycastCard(end);
            if (card == null)
            {
                if (observedCard != null)
                {
                    hoverCard.Hide();
                    observedCard = null;
                }
                return;
            }
            if (card != observedCard)
            {
                observedCard = card;
                if (observedCard.isInCoolDown)
                {
                    hoverCardPos = new Vector3(observedCard.transform.position.x, observedCard.transform.position.y, -1f);
                }
                else
                {
                    float y = cardAllignment.GetCentralControlPos().y;
                    hoverCardPos = new Vector3(card.transform.position.x, y, -1f);
                }
                hoverCard.Show(observedCard, hoverCardPos);
            }
        }
        void MoveHoverCard(Vector3 start, Vector3 end)
        {
            Vector3 offset = camera.ScreenToWorldPoint(end) - camera.ScreenToWorldPoint(start);
            hoverCard.ChangePosition(hoverCardPos + new Vector3(offset.x, offset.y, 0));
        }
        #endregion

        void SelectClick(Vector3 screenPos)
        {
            var card = RaycastCard(screenPos);
            if (card != null)
            {
                onSelectCard.Invoke();
            }
        }



        bool UseCardCheck(Vector3 screenPos)
        {
            return camera.ScreenToWorldPoint(screenPos).y > 0 && fightControl.isNeedResponse;
        }
        void UseCard()
        {
            cardController.UsingCard(selectedCard);
            fightControl.UseCard(selectedCard);
        }



        MagicCard RaycastCard(Vector3 screenPos)
        {
            Ray ray = new Ray(camera.ScreenToWorldPoint(screenPos), Vector3.forward);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 10f);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
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
    }
}