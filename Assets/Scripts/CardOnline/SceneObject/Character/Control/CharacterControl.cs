using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BF;
using System;
using CardOnline.Card;
using CardOnline.Bullet;

namespace CardOnline.Character
{
	public class CharacterControl : BaseControl
	{
		CharacterData characterData;
		public CharacterData Data => characterData;


		//
		public void OnBeHit(BaseBullet baseBullet) => characterData.onBeHit.Invoke(baseBullet);


		protected override void Awake() 
		{
			base.Awake();
			characterData = GetComponentInChildren<CharacterData>();
		}
		public override void Initialize<T>(T para){

		}
		public override void Open(){
			characterData.Open();
		}

        public override void Close()
        {
            
        }
    }
}