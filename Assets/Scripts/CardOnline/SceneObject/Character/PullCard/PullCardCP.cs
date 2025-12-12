using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BF;

namespace CardOnline.Character
{
 	public class PullCardCP : BaseComponent
	{
        CharacterData characterData;

        [SerializeField] bool isAttack;
        [SerializeField] bool isDefense;

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

        public void Attack()
        {


        }
        public void Defense()
        {

        }
	}
}
