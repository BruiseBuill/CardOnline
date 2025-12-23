using CardOnline.Card;
using CardOnline.Character;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.Bullet
{
	public class BaseBullet : MonoBehaviour
	{
		public CharacterControl parent;
		public CharacterControl target;
		public Transform targetTrans;
        public int power;
		public bool isAttack;

		public List<Effect> effectList = new List<Effect>();

		[Header("Animation")]
        public float prepareDuration = 0.15f;
        public float dashDuration = 0.25f;
        public float hitStopTime = 0.05f;
        Vector3 cardOriginPos;
        Vector3 cardOriginScale;



        public void PlayAttackAnimation()
		{
            cardOriginPos = targetTrans.position;
            cardOriginScale = targetTrans.localScale;

            Sequence seq = DOTween.Sequence();

            // 1. 蓄力：后拉 + 放大
            seq.Append(targetTrans.DOScale(cardOriginScale * 1.1f, prepareDuration))
               .Join(targetTrans.DOMove(targetTrans.position - targetTrans.right * 40f, prepareDuration)
                   .SetEase(Ease.OutQuad));

            // 2. 冲刺：高速撞向敌人
            seq.Append(targetTrans.DOMove(target.transform.position, dashDuration)
                   .SetEase(Ease.InBack));

            // 3. 收尾：卡牌回原位并恢复
            seq.Append(targetTrans.DOMove(cardOriginPos, 0.2f))
               .Join(targetTrans.DOScale(cardOriginScale, 0.2f));

            seq.onComplete += AttackComplete;

            seq.Play();
        }
        void AttackComplete()
        {

        }
	}
}