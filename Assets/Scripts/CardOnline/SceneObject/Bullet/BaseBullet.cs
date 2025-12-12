using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardOnline.Character;

namespace CardOnline.Bullet
{
	public class BaseBullet : MonoBehaviour
	{
		public CharacterControl parent;
		public CharacterControl target;
        public int power;
		public bool isAttack;

		public List<Effect> effectList = new List<Effect>();
		
	}
}