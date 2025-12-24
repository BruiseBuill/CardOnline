using BF;
using CardOnline.Character;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CardOnline.UI
{
	public class UI_FightTip : MonoBehaviour
	{
        public RectTransform textRect;

        public CanvasGroup textCanvasGroup;
        public CanvasGroup attackImageCanvasGroup;
        public CanvasGroup defendImageCanvasGroup;

        public TextMeshProUGUI tipText;

        string attackTip = "进攻开始";
        string defendTip = "防守开始";

        [Header("In_Event")]
        [SerializeField] protected GenericEventChannel<object> ch_ActionStart;



        private void Start()
        {
            ch_ActionStart.AddListener(OnActionStart);
        }
        private void OnDestroy()
        {
            ch_ActionStart.RemoveListener(OnActionStart);
        }
        void OnActionStart(object obj)
        {
            var ch = obj as CharacterControl;
            if (ch.Data.isPlayer)
            {
                PlayAttackAniamtion();
            }
            else
            {
                PlayDefendAniamtion();
            }
        }
        void PlayAttackAniamtion()
        {
            tipText.text = attackTip;
            PlayTextAnimation(textRect, textCanvasGroup);
            PlayAttackImageAnimation();
        }
        void PlayDefendAniamtion()
        {
            tipText.text = defendTip;
            PlayTextAnimation(textRect, textCanvasGroup);
            PlayDefendImageAnimation();
        }
        public void StopAttackAniamtion()
        {
            attackImageCanvasGroup.DOFade(0f, 0.2f);
        }
        public void StopDefendAniamtion()
        {
            defendImageCanvasGroup.DOFade(0f, 0.2f);
        }   
        void PlayTextAnimation(RectTransform uiRect, CanvasGroup canvasGroup)
        {
            uiRect.anchoredPosition = Vector2.zero;
            canvasGroup.alpha = 0f;

            // 3️⃣ DOTween 序列
            Sequence seq = DOTween.Sequence();

            // Alpha 渐显
            seq.Append(
                canvasGroup.DOFade(1f, 0.5f)
            );

            // 完全显示后，快速向左飞出屏幕
            seq.Append(
                uiRect.DOAnchorPosX(-Screen.width, 0.3f)
                      .SetEase(Ease.InCubic)
            );
            seq.Append(
                canvasGroup.DOFade(0f, 0.2f)
            );
            seq.Play();
        }
        void PlayAttackImageAnimation()
        {
            attackImageCanvasGroup.alpha = 0f;
            Sequence seq = DOTween.Sequence();
            seq.Append(
                attackImageCanvasGroup.DOFade(1f, 0.5f)
            );
        }
        void PlayDefendImageAnimation()
        {
            defendImageCanvasGroup.alpha = 0f;
            Sequence seq = DOTween.Sequence();
            seq.Append(
                defendImageCanvasGroup.DOFade(1f, 0.5f)
            );
        }
    }
}