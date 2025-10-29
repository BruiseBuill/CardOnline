using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardOnline.Card
{
	public class MagicCard : MonoBehaviour
	{
        [SerializeField] MagicCardData cardData;
        public MagicCardData CardData => cardData;

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI powerText;
        public TextMeshProUGUI coolDownText;
        public TextMeshProUGUI descriptionText;

        public void SetData(MagicCardData cardData)
        {
            this.cardData = cardData;
        }
        [ContextMenu("Load")]
        public void Load()
        {
            nameText.text = cardData.name;
            powerText.text=cardData.power;
            coolDownText.text = cardData.coolDown;
            descriptionText.text = cardData.description;
        }
        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}