using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.Card
{
	public class CardAniamtion : MonoBehaviour
	{
        [Header("Animation")]
        public Transform targetTrans;
        public float prepareDuration = 0.15f;
        public float dashDuration = 0.25f;
        public float hitStopTime = 0.05f;
        Vector3 cardOriginPos;
        Vector3 cardOriginScale;

        void PlayAttackAnimation()
        {
            cardOriginPos = targetTrans.position;
            cardOriginScale = targetTrans.localScale;

            Sequence seq = DOTween.Sequence();

            // 1. 蓄力：后拉 + 放大
            seq.Append(targetTrans.DOScale(cardOriginScale * 1.1f, prepareDuration))
               .Join(targetTrans.DOMove(targetTrans.position - targetTrans.right * 40f, prepareDuration)
                   .SetEase(Ease.OutQuad));

            // 2. 冲刺：高速撞向敌人
            seq.Append(targetTrans.DOMove(targetTrans.position, dashDuration)
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