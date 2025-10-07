using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

namespace CardOnline.Player
{
    public class CardAllignment : MonoBehaviour
    {
        [SerializeField] List<GameObject> cards = new List<GameObject>();

        [Header("贝塞尔曲线控制点")]
        [SerializeField] private Transform startPoint;      // 起始点
        [SerializeField] private Transform controlPoint1;   // 控制点1
        [SerializeField] private Transform controlPoint2;   // 控制点2
        [SerializeField] private Transform endPoint;        // 结束点
        [SerializeField] private float baseSpacing = 0.1f;  // 基础间距

        [SerializeField] private float duration = 1f;

        [Header("可视化设置")]
        [SerializeField] private bool showCurve = true;     // 显示曲线
        [SerializeField] private Color curveColor = Color.red; // 曲线颜色
        [SerializeField] private int curveResolution = 50;  // 曲线分辨率
        [SerializeField] private bool showControlPoints = true; // 显示控制点

        /// <summary>
        /// 计算卡牌在弧线上的位置参数t
        /// </summary>
        /// <param name="cardIndex">卡牌索引</param>
        /// <returns>贝塞尔曲线参数t</returns>
        private float CalculateCardT(int cardIndex)
        {
            return 0.5f - (cards.Count - 1) * 0.5f * baseSpacing + cardIndex * baseSpacing;
        }

        [ContextMenu("Update")]
        private void UpdateCardPositions()
        {
            if (cards.Count == 0) return;

            for (int i = 0; i < cards.Count; i++)
            {
                // 使用新的间距计算方法来获取卡牌在弧线上的位置
                float t = CalculateCardT(i);
                Vector3 position = CalculateBezierPoint(t);
                Vector3 tangent = CalculateBezierTangent(t);

                cards[i].transform.DOMove(position, duration);

                // 计算卡牌旋转角度（沿切线方向）
                float angle = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;
                cards[i].transform.DORotate(new Vector3(0, 0, angle), duration);
            }
        }


        /// <summary>
        /// 计算贝塞尔曲线上的点
        /// </summary>
        /// <param name="t">参数t (0-1)</param>
        private Vector3 CalculateBezierPoint(float t)
        {
            Vector3 p0 = startPoint.localPosition;
            Vector3 p1 = controlPoint1.localPosition;
            Vector3 p2 = controlPoint2.localPosition;
            Vector3 p3 = endPoint.localPosition;

            // 三次贝塞尔曲线公式
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 point = uuu * p0;
            point += 3 * uu * t * p1;
            point += 3 * u * tt * p2;
            point += ttt * p3;

            return point;
        }

        /// <summary>
        /// 计算贝塞尔曲线的切线方向
        /// </summary>
        /// <param name="t">参数t (0-1)</param>
        /// <returns>切线方向向量</returns>
        private Vector3 CalculateBezierTangent(float t)
        {
            Vector3 p0 = startPoint.localPosition;
            Vector3 p1 = controlPoint1.localPosition;
            Vector3 p2 = controlPoint2.localPosition;
            Vector3 p3 = endPoint.localPosition;

            // 三次贝塞尔曲线的导数
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector3 tangent = 3 * uu * (p1 - p0);
            tangent += 6 * u * t * (p2 - p1);
            tangent += 3 * tt * (p3 - p2);

            return tangent.normalized;
        }

        /// <summary>
        /// 在Scene视图中绘制控制点
        /// </summary>
        void OnDrawGizmos()
        {
            if (!showControlPoints) return;

            // 绘制控制点
            Gizmos.color = Color.red;
            if (startPoint != null) Gizmos.DrawWireSphere(startPoint.position, 0.1f);
            if (endPoint != null) Gizmos.DrawWireSphere(endPoint.position, 0.1f);

            Gizmos.color = Color.yellow;
            if (controlPoint1 != null) Gizmos.DrawWireSphere(controlPoint1.position, 0.08f);
            if (controlPoint2 != null) Gizmos.DrawWireSphere(controlPoint2.position, 0.08f);

            // 绘制控制线
            Gizmos.color = Color.gray;
            if (startPoint != null && controlPoint1 != null)
                Gizmos.DrawLine(startPoint.position, controlPoint1.position);
            if (controlPoint1 != null && controlPoint2 != null)
                Gizmos.DrawLine(controlPoint1.position, controlPoint2.position);
            if (controlPoint2 != null && endPoint != null)
                Gizmos.DrawLine(controlPoint2.position, endPoint.position);

            // 绘制贝塞尔曲线
            if (startPoint != null && controlPoint1 != null && controlPoint2 != null && endPoint != null)
            {
                Gizmos.color = curveColor;
                Vector3 previousPoint = CalculateBezierPoint(0);
                for (int i = 1; i <= 50; i++)
                {
                    float t = (float)i / 50;
                    Vector3 currentPoint = CalculateBezierPoint(t);
                    Gizmos.DrawLine(previousPoint, currentPoint);
                    previousPoint = currentPoint;
                }
            }
        }
    }
}