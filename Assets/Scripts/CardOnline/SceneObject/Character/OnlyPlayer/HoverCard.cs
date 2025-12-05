using CardOnline.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.Character
{
    public class HoverCard : MonoBehaviour
    {
        public MagicCard hoverCard;

        public void Show(MagicCard card,Vector3 pos)
        {
            hoverCard.SetData(card.CardData);
            hoverCard.transform.position = pos;
            hoverCard.Show();
        }
        public void Hide()
        {
            hoverCard.Hide();
        }
        public void ChangePosition(Vector3 pos)
        {

            hoverCard.transform.position = pos;
        }
    }
}