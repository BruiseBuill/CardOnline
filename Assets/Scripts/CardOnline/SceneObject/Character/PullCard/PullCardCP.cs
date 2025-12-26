using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BF;
using CardOnline.Card;

namespace CardOnline.Character
{
 	public class PullCardCP : BaseComponent
	{
        protected CharacterData characterData;

        [SerializeField] bool isAttack;
        [SerializeField] bool isDefense;
        [SerializeField] CharacterControl target;

        protected override void Awake()
        {
            base.Awake();
            characterData = data as CharacterData;
        }
        public override void Open()
        {
            

        }
        public override void Close()
        {
            
        }

        public void Attack(MagicCard card)
        {
            if (target == null)
            {
                return;
            }
            var bullet = PoolManager.Instance().Release(card.CardData.bulletName);

        }
        public void Defense()
        {

        }
	}
}
