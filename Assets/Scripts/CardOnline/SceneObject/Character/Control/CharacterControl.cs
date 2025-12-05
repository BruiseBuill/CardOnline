using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BF;
using System;
using CardOnline.Card;

namespace CardOnline.Character
{
	public class CharacterControl : BaseControl
	{
		CharacterData characterData;
		protected CharacterCardSystem characterCardSystem;


		[Header("Event")]
		public Action<MagicCard> onBeenAccelerate;
		public Action<MagicCard> onBeenAccelerateArea;

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
            throw new System.NotImplementedException();
        }
    }
}