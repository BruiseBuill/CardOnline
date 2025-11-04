using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace CardOnline.View
{
	public class CoolDownAlligement : MonoBehaviour
	{
		[SerializeField] List<GameObject> cards_1;
		[SerializeField] List<GameObject> cards_2;
		[SerializeField] List<GameObject> cards_3;
		[SerializeField] List<GameObject> cards_4;

		[SerializeField] Transform point_1;
        [SerializeField] Transform point_2;
        [SerializeField] Transform point_3;
        [SerializeField] Transform point_4;

        [SerializeField] float spacing = 0.5f;

        [SerializeField] float duration = 1f;
        [SerializeField] Vector3 scale = new Vector3(1, 1, 1);

        [SerializeField] CardAllignment cardAllignment;

        public void AccelerateAll()
        {
            AccelerateArea(1);
            AccelerateArea(2);
            AccelerateArea(3);
            AccelerateArea(4);
        }
        public void AccelerateArea(int remainCD)
        {
            switch (remainCD)
            {
                case 4:
                    while (cards_4.Count > 0)
                    {
                        AccelerateOneCard(cards_4[0], remainCD);
                    }
                    break;
                case 3:
                    while (cards_3.Count > 0)
                    {
                        AccelerateOneCard(cards_3[0], remainCD);
                    }
                    break;
                case 2:
                    while (cards_2.Count > 0)
                    {
                        AccelerateOneCard(cards_2[0], remainCD);
                    }
                    break;
                case 1:
                    while (cards_1.Count > 0)
                    {
                        AccelerateOneCard(cards_1[0], remainCD);
                    }
                    break;
            }
        }
        public void AccelerateOneCard(GameObject card,int remainCD)
        {
            if (remainCD > 1)
            {
                RemoveCard(card, remainCD);
                AddCard(card, remainCD - 1);
            }
            else
            {
                RemoveCard(card, remainCD);
                cardAllignment.AddCard(card);
            }
        }
        public void SlowOneCard(GameObject card,int remainCD)
        {
            RemoveCard(card, remainCD);
            AddCard(card, remainCD + 1);
        }
        public void SlowArea(int remainCD)
        {
            switch (remainCD)
            {
                case 1:
                    while (cards_1.Count > 0)
                    {
                        SlowOneCard(cards_1[0], remainCD);
                    }
                    break;
                case 2:
                    while (cards_2.Count > 0)
                    {
                        SlowOneCard(cards_2[0], remainCD);
                    }
                    break;
                case 3:
                    while (cards_3.Count > 0)
                    {
                        SlowOneCard(cards_3[0], remainCD);
                    }
                    break;
            }
        }
        void UpdateCardPositions(List<GameObject> cards, Transform startPoint)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Vector3 targetPos = startPoint.position + new Vector3(0, -i * spacing, -0.02f);
                cards[i].transform.DOMove(targetPos, duration);
                cards[i].transform.DORotate(Vector3.zero, duration);
            }
        }
        #region Add and Remove
        public void AddCard(GameObject card, int cd)
        {
            card.transform.DOScale(scale, duration);
            switch (cd)
            {
                case 1:
                    cards_1.Add(card);
                    UpdateCardPositions(cards_1, point_1);
                    break;
                case 2:
                    cards_2.Add(card);
                    UpdateCardPositions(cards_2, point_2);
                    break;
                case 3:
                    cards_3.Add(card);
                    UpdateCardPositions(cards_3, point_3);
                    break;
                case 4:
                    cards_4.Add(card);
                    UpdateCardPositions(cards_4, point_4);
                    break;
            }
        }
        public void RemoveCard(GameObject card, int cd)
        {
            card.transform.DOScale(Vector3.one, duration);
            switch (cd)
            {
                case 1:
                    cards_1.Remove(card);
                    UpdateCardPositions(cards_1, point_1);
                    break;
                case 2:
                    cards_2.Remove(card);
                    UpdateCardPositions(cards_2, point_2);
                    break;
                case 3:
                    cards_3.Remove(card);
                    UpdateCardPositions(cards_3, point_3);
                    break;
                case 4:
                    cards_4.Remove(card);
                    UpdateCardPositions(cards_4, point_4);
                    break;
            }
        }
        #endregion
        
    }
}