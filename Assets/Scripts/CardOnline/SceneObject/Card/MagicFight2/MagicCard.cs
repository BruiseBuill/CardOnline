using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CardOnline.Character;

namespace CardOnline
{
    public enum LayerOrder
    {
        BG=0,
        Model=10,
        UI=20,
        ModelOnUI=30
    }
}

namespace CardOnline.Card
{
	public class MagicCard : MonoBehaviour
	{
        [SerializeField] MagicCardData cardData;
        public MagicCardData CardData => cardData;

        [Header("Model")]
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI powerText;
        public TextMeshProUGUI coolDownText;
        public TextMeshProUGUI descriptionText;
        public Canvas canvas;

        [Header("FightInfo")]
        public CharacterControl characterControl;
        public bool isInCoolDown = false;
        public int remainCoolingTime;

        private void Start()
        {
            SetSortingOrder((int)LayerOrder.Model);
            if (characterControl == null)
            {
                characterControl = GameObject.FindFirstObjectByType<CharacterControl>();
            }
        }
        public void Open()
        {

        }
        public void SetData(MagicCardData cardData)
        {
            this.cardData = cardData;
            Load();
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

        public void SetSortingOrder(int order)
        {
            canvas.sortingOrder = order;
        }
    }
}