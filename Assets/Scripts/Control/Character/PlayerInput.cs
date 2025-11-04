 using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardOnline.Card;
using Sirenix.OdinInspector;
using CardOnline.Control;
using CardOnline.View;
using CardOnline.Character;
using DG.Tweening;

namespace CardOnline.Player
{
	public class PlayerInput : MonoBehaviour
	{
        public MagicCard hoverCard;
        [ReadOnly]
        public MagicCard selectedCard;
        [ReadOnly]
        public MagicCard observedCard;

        public bool isSelected = false; //是否选中
        public bool isObserved = false;
        public bool hasTarget = false; //是否有目标

        Vector3 hoverCardInitPos;
        Camera camera;

        [SerializeField] CardAllignment cardAllignment;
        [SerializeField] CharacterCardController cardController;

        [SerializeField] float duration = 0.5f;

        [Header("Event")]
        [SerializeField] GenericEventChannel<bool> onCloseRaycast;


        private void Start()
        {
            camera = Camera.main;
            hoverCard.Hide();

            InputManager.onPointerDown += OnPointerDown;
            InputManager.onPointerUp += OnPointerUp;
            InputManager.onDrag += OnPointerDrag;
        }
        void OnPointerDown(Vector3 screenPos)
        {
            MagicCard card = RaycastCard(screenPos);
            if (card == null)
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
            hoverCardInitPos = new Vector3(card.transform.position.x, y, -1f);
            hoverCard.transform.position = hoverCardInitPos;
            hoverCard.SetData(card.CardData);
            hoverCard.Show();
        }
        void OnPointerUp(Vector3 pos)
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
            selectedCard.transform.rotation = hoverCard.transform.rotation;
            selectedCard.Show();
            cardAllignment.UpdateCardPositions();
            
            if (AttackCheck(pos))
            {
                Attack();
            }
            isSelected = false;
            selectedCard = null;
        }
        void OnPointerDrag(Vector3 start,Vector3 end)
        {
            if (!isSelected)
            {
                return;
            }
            Vector3 offset = camera.ScreenToWorldPoint(end) - camera.ScreenToWorldPoint(start);
            hoverCard.transform.position = hoverCardInitPos + new Vector3(offset.x, offset.y, 0);
        }
        private void Update()
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
                    hoverCard.SetData(observedCard.CardData);
                    if (observedCard.isInCoolDown)
                    {
                        hoverCard.transform.position = new Vector3(observedCard.transform.position.x, observedCard.transform.position.y, -1f);
                    }
                    else
                    {
                        float y = cardAllignment.GetCentralControlPos().y;
                        hoverCardInitPos = new Vector3(card.transform.position.x, y, -1f);
                        hoverCard.transform.position = hoverCardInitPos;                       
                    }
                    hoverCard.Show();
                }
            }
        }
        bool AttackCheck(Vector3 screenPos)
        {
            return camera.ScreenToWorldPoint(screenPos).y > 0; 
        }
        void Attack()
        {
            cardController.UsingCard(selectedCard);
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