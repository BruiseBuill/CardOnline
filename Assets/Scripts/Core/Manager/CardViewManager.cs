using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.Player
{
	public class CardViewManager : Single<CardViewManager>
	{

        public GameObject hoverCard;

        private void Start()
        {
            hoverCard.SetActive(false);

            InputManager.onPointerDown+=OnPointerDown;
        }
        void OnPointerDown(Vector3 pos){
            MagicCard card = Raycast(pos);
            if (card == null)
            {
                Debug.Log("Raycast miss");
            }

            hoverCard.SetActive(true);
            Vector3 y = CardAllignment.Instance().GetCentralControlPos().y;
            hoverCard.transform.position = new Vector3(card.transform.position.x,y,card.transform.position.z);
        }
        MagicCard Raycast(Vector3 pos)
        {
			Ray ray = new Ray(pos, Vector3.back);
			if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
			{
				Collider collider = hitInfo.collider;
				Debug.Log($"Raycast hit: {collider.name}");
                MagicCard card = collider.GetComponent<MagicCard>();
                return card;
			}
            else
            {
                return null;
            }
        }
        private void Update()
        {
            
        }
    }
}