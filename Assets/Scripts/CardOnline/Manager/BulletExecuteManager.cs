using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BF;
using CardOnline.Bullet;

namespace CardOnline.Manager
{
	public class BulletExecuteManager : Single<BulletExecuteManager>
	{
		[SerializeField] bool isExecuteOver;
		[SerializeField] bool isAllExecuteOver;

		List<Effect> effectList;

		public void ExecuteBullet(BaseBullet baseBullet)
		{
			effectList = baseBullet.effectList;
			StartCoroutine("ExecuteEffectList");
		}

		IEnumerator ExecuteEffectList()
		{
			foreach(var effect in effectList)
			{
				isExecuteOver = false;
				effect.Execute();
				yield return new WaitUntil(() => isExecuteOver);
			}
			isExecuteOver= true;
		}

	}
}