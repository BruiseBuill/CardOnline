using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardOnline.Card;
using Sirenix.OdinInspector;
using CardOnline.View;
using CardOnline.Character;
using System;

namespace CardOnline.Player
{
    [Serializable]
    public class PlayerInput_Silence : PlayerInputMode
    {
        public HoverCard hoverCard;
        [ReadOnly]
        public MagicCard selectedCard;
        [ReadOnly]
        public MagicCard observedCard;

        public bool isSelected = false; //是否选中
        public bool isObserved = false;
        public bool hasTarget = false; //是否有目标

        Vector3 hoverCardPos;
        Camera camera;

        [SerializeField] CardAllignment cardAllignment;
        [SerializeField] CharacterCardController cardController;
        [SerializeField] FightControl fightControl;

        [SerializeField] float duration = 0.5f;

        [Header("Event")]
        [SerializeField] GenericEventChannel<bool> onCloseRaycast;

        public PlayerInput_Silence(PlayerInput playerInput) : base(playerInput)
        {

        }

        public override void EnterInputMode()
        {
            camera = Camera.main;
            hoverCard.Hide();

            InputManager.onPointerDown += OnPointerDown;
            InputManager.onPointerUp += OnPointerUp;
            InputManager.onDrag += OnPointerDrag;
        }
        public override void ExitInputMode()
        {
            hoverCard.Hide();
            InputManager.onPointerDown -= OnPointerDown;
            InputManager.onPointerUp -= OnPointerUp;
            InputManager.onDrag -= OnPointerDrag;
        }
        public override void OnPointerDown(Vector3 screenPos)
        {
            MagicCard card = RaycastCard(screenPos);
            if (card == null || card.isInCoolDown)
            {
                isSelected = false;
                isObserved = true;
                return;
            }

            onCloseRaycast.Invoke(true);

            isSelected = true;
            selectedCard = card;

            card.Hide();

            float y = cardAllignment.GetCentralControlPos().y;
            hoverCardPos = new Vector3(card.transform.position.x, y, -1f);
            hoverCard.Show(card, hoverCardPos);
        }
        public override void OnPointerUp(Vector3 pos)
        {
            if (!isSelected)
            {
                if (isObserved)
                {
                    isObserved = false;
                    hoverCard.Hide();
                    observedCard = null;
                }
                return;
            }
            onCloseRaycast.Invoke(false);
            hoverCard.Hide();

            selectedCard.transform.position = hoverCard.transform.position;
            selectedCard.transform.rotation = Quaternion.identity;
            selectedCard.Show();
            cardAllignment.UpdateCardPositions();

            if (UseCardCheck(pos))
            {
                UseCard();
            }
            isSelected = false;
            selectedCard = null;
        }
        public override void OnPointerDrag(Vector3 start, Vector3 end)
        {
            if (!isSelected)
            {
                return;
            }
            Vector3 offset = camera.ScreenToWorldPoint(end) - camera.ScreenToWorldPoint(start);
            hoverCard.ChangePosition(hoverCardPos + new Vector3(offset.x, offset.y, 0));
        }
        public override void OnUpdate()
        {
            if (isObserved)
            {
                MagicCard card = RaycastCard(Input.mousePosition);
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
        MagicCard RaycastCard(Vector3 pos)
        {
            Ray ray = new Ray(camera.ScreenToWorldPoint(pos), Vector3.forward);
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
