using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardOnline.Bullet;

namespace CardOnline.Manager
{
	public class FightManager : Single<FightManager>
	{
		[SerializeField] BaseBullet attackBullet;
		[SerializeField] BaseBullet defenseBullet;

		public void SetAttackBullet(BaseBullet bullet)
		{
			attackBullet = bullet;
        }
        public void SetDefenseBullet(BaseBullet bullet)
        {
            defenseBullet = bullet;
			Calculate();
        }
		public void DontAttack()
		{

		}
		public void DontDefense()
		{
			Calculate();

        }

		void Calculate()
		{
			if (defenseBullet == null || defenseBullet.power < attackBullet.power) 
			{
				AttackerWin();
			}
			else
			{
				DefenserWin();
			}

		}
		void AttackerWin()
		{

		}
		void DefenserWin()
		{

		}
    }
}