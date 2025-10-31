using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardOnline.Card;
using Sirenix.OdinInspector;

namespace CardOnline.Player
{
	public class CardViewManager : Single<CardViewManager>
	{
        public MagicCard hoverCard;
        [ReadOnly]
        public MagicCard selectedCard;
        public bool isSelected = false; //是否选中
        public bool hasTarget = false; //是否有目标

        Vector3 hoverCardInitPos;
        Camera camera;

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
                return;
            }

            onCloseRaycast.Invoke(true);

            isSelected = true;
            selectedCard = card;

            card.Hide();

            float y = CardAllignment.Instance().GetCentralControlPos().y;
            hoverCardInitPos = new Vector3(card.transform.position.x, y, -1f);
            hoverCard.transform.position = hoverCardInitPos;
            hoverCard.SetData(card.CardData);
            hoverCard.Show();
        }
        void OnPointerUp(Vector3 pos)
        {
            if (!isSelected)
            {
                return;
            }

            onCloseRaycast.Invoke(false);

            hoverCard.Hide();
            selectedCard.Show();
            isSelected = false;
            selectedCard = null;
        }
        void OnPointerDrag(Vector3 start,Vector3 end)
        {
            if (!isSelected)
            {
                return;
            }
            Vector3 offset = camera.ScreenToWorldPoint(end)- camera.ScreenToWorldPoint(start);
            hoverCard.transform.position = hoverCardInitPos + new Vector3(offset.x, offset.y, 0);
        }
        void Attack()
        {

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