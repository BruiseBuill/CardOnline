using BF;
using CardOnline.Card;
using CardOnline.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.Character
{
	public class CharacterCardController : MonoBehaviour
	{
        [SerializeField] EventChannel ch_StartTurn;
		[SerializeField] EventChannel ch_EndTurn;
        [SerializeField] CardAllignment cardAllignment;
        [SerializeField] CoolDownAlligement coolDownAlligement;

        [SerializeField] List<MagicCard> handCardsList;
        [SerializeField] List<MagicCard> coolCards_1;
        [SerializeField] List<MagicCard> coolCards_2;
        [SerializeField] List<MagicCard> coolCards_3;
        [SerializeField] List<MagicCard> coolCards_4;

        private void Start()
        {
            ch_StartTurn.AddListener(OnStartTurn);
            ch_EndTurn.AddListener(OnEndTurn);
        }
        private void OnDestroy()
        {
            ch_StartTurn.RemoveListener(OnStartTurn);
            ch_EndTurn.RemoveListener(OnEndTurn);
        }
        void OnStartTurn()
        {
            coolDownAlligement.AccelerateAll();

            for (int i = 0; i < coolCards_1.Count; i++)
            {
                coolCards_1[i].isInCoolDown = false;
                handCardsList.Add(coolCards_1[i]);
            }
            coolCards_1 = coolCards_2;
            coolCards_2 = coolCards_3;
            coolCards_3 = coolCards_4;
            coolCards_4.Clear();
            foreach(var card in coolCards_1)
            {
                card.remainCoolingTime = 1;
            }
            foreach (var card in coolCards_2)
            {
                card.remainCoolingTime = 2;
            }
            foreach (var card in coolCards_3)
            {
                card.remainCoolingTime = 3;
            }
        }
		private void OnEndTurn()
		{
            
        }
        public void UsingCard(MagicCard card)
        {
            handCardsList.Remove(card);
            cardAllignment.RemoveCard(card.gameObject);
            card.remainCoolingTime = int.Parse(card.CardData.coolDown);
            card.isInCoolDown = true;
            switch (card.remainCoolingTime)
            {
                case 1:
                    coolCards_1.Add(card);
                    coolDownAlligement.AddCard(card.gameObject, 1);
                    break;
                case 2:
                    coolCards_2.Add(card);
                    coolDownAlligement.AddCard(card.gameObject, 2);
                    break;
                case 3:
                    coolCards_3.Add(card);
                    coolDownAlligement.AddCard(card.gameObject, 3);
                    break;
                case 4:
                    coolCards_4.Add(card);
                    coolDownAlligement.AddCard(card.gameObject, 4);
                    break;
            }
        }
    }
}