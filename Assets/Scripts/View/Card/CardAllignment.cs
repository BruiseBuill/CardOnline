using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline
{
    /// <summary>
    /// 卡牌弧线排列系统 - 使用贝塞尔曲线进行排列
    /// </summary>
    public class CardAllignment : MonoBehaviour
    {
        [Header("贝塞尔曲线控制点")]
        [SerializeField] private Transform startPoint;      // 起始点
        [SerializeField] private Transform controlPoint1;   // 控制点1
        [SerializeField] private Transform controlPoint2;   // 控制点2
        [SerializeField] private Transform endPoint;        // 结束点
        
        [Header("卡牌设置")]
        [SerializeField] private GameObject cardPrefab;     // 卡牌预制体
        [SerializeField] private int cardCount = 5;         // 卡牌数量
        [SerializeField] private float cardSpacing = 0.1f;  // 卡牌间距
        
        [Header("可视化设置")]
        [SerializeField] private bool showCurve = true;     // 显示曲线
        [SerializeField] private Color curveColor = Color.red; // 曲线颜色
        [SerializeField] private int curveResolution = 50;  // 曲线分辨率
        [SerializeField] private bool showControlPoints = true; // 显示控制点
        
        [Header("卡牌旋转设置")]
        [SerializeField] private bool enableCardRotation = true; // 启用卡牌旋转
        [SerializeField] private float rotationOffset = 0f;     // 旋转偏移
        
        private List<GameObject> cards = new List<GameObject>();
        private LineRenderer lineRenderer;
        
        void Start()
        {
            InitializeControlPoints();
            CreateLineRenderer();
            GenerateCards();
        }
        
        void Update()
        {
            UpdateCardPositions();
            UpdateVisualization();
        }
        
        /// <summary>
        /// 初始化控制点（如果没有设置的话）
        /// </summary>
        private void InitializeControlPoints()
        {
            if (startPoint == null)
            {
                GameObject start = new GameObject("StartPoint");
                start.transform.SetParent(transform);
                start.transform.localPosition = new Vector3(-2f, 0f, 0f);
                startPoint = start.transform;
            }
            
            if (controlPoint1 == null)
            {
                GameObject cp1 = new GameObject("ControlPoint1");
                cp1.transform.SetParent(transform);
                cp1.transform.localPosition = new Vector3(-1f, 2f, 0f);
                controlPoint1 = cp1.transform;
            }
            
            if (controlPoint2 == null)
            {
                GameObject cp2 = new GameObject("ControlPoint2");
                cp2.transform.SetParent(transform);
                cp2.transform.localPosition = new Vector3(1f, 2f, 0f);
                controlPoint2 = cp2.transform;
            }
            
            if (endPoint == null)
            {
                GameObject end = new GameObject("EndPoint");
                end.transform.SetParent(transform);
                end.transform.localPosition = new Vector3(2f, 0f, 0f);
                endPoint = end.transform;
            }
        }
        
        /// <summary>
        /// 创建线条渲染器用于可视化
        /// </summary>
        private void CreateLineRenderer()
        {
            GameObject lineObject = new GameObject("BezierCurveLine");
            lineObject.transform.SetParent(transform);
            lineRenderer = lineObject.AddComponent<LineRenderer>();
            
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.color = curveColor;
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.positionCount = curveResolution;
            lineRenderer.useWorldSpace = false;
        }
        
        /// <summary>
        /// 生成卡牌
        /// </summary>
        private void GenerateCards()
        {
            // 清除现有卡牌
            foreach (GameObject card in cards)
            {
                if (card != null)
                    DestroyImmediate(card);
            }
            cards.Clear();
            
            // 生成新卡牌
            for (int i = 0; i < cardCount; i++)
            {
                GameObject card;
                if (cardPrefab != null)
                {
                    card = Instantiate(cardPrefab, transform);
                }
                else
                {
                    // 创建默认卡牌
                    card = CreateDefaultCard();
                }
                
                card.name = $"Card_{i}";
                cards.Add(card);
            }
        }
        
        /// <summary>
        /// 创建默认卡牌（如果没有预制体）
        /// </summary>
        private GameObject CreateDefaultCard()
        {
            GameObject card = GameObject.CreatePrimitive(PrimitiveType.Cube);
            card.transform.SetParent(transform);
            card.transform.localScale = new Vector3(0.5f, 0.7f, 0.1f);
            
            // 添加卡牌组件
            if (card.GetComponent<MagicFighting2.MagicCard>() == null)
            {
                card.AddComponent<MagicFighting2.MagicCard>();
            }
            
            return card;
        }
        
        /// <summary>
        /// 更新卡牌位置
        /// </summary>
        private void UpdateCardPositions()
        {
            if (cards.Count == 0) return;
            
            for (int i = 0; i < cards.Count; i++)
            {
                float t = (float)i / (cards.Count - 1);
                Vector3 position = CalculateBezierPoint(t);
                Vector3 tangent = CalculateBezierTangent(t);
                
                cards[i].transform.localPosition = position;
                
                // 设置卡牌旋转
                if (enableCardRotation)
                {
                    float angle = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg + rotationOffset;
                    cards[i].transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
        }
        
        /// <summary>
        /// 更新可视化
        /// </summary>
        private void UpdateVisualization()
        {
            if (lineRenderer == null) return;
            
            if (showCurve)
            {
                lineRenderer.enabled = true;
                for (int i = 0; i < curveResolution; i++)
                {
                    float t = (float)i / (curveResolution - 1);
                    Vector3 point = CalculateBezierPoint(t);
                    lineRenderer.SetPosition(i, point);
                }
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
        
        /// <summary>
        /// 计算贝塞尔曲线上的点
        /// </summary>
        /// <param name="t">参数t (0-1)</param>
        /// <returns>曲线上的点</returns>
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
        
        /// <summary>
        /// 重新生成卡牌
        /// </summary>
        [ContextMenu("重新生成卡牌")]
        public void RegenerateCards()
        {
            GenerateCards();
        }
        
        /// <summary>
        /// 设置卡牌数量
        /// </summary>
        /// <param name="count">卡牌数量</param>
        public void SetCardCount(int count)
        {
            cardCount = Mathf.Max(1, count);
            GenerateCards();
        }
        
        /// <summary>
        /// 获取指定位置的卡牌
        /// </summary>
        /// <param name="index">卡牌索引</param>
        /// <returns>卡牌GameObject</returns>
        public GameObject GetCard(int index)
        {
            if (index >= 0 && index < cards.Count)
                return cards[index];
            return null;
        }
        
        /// <summary>
        /// 获取所有卡牌
        /// </summary>
        /// <returns>卡牌列表</returns>
        public List<GameObject> GetAllCards()
        {
            return new List<GameObject>(cards);
        }
    }
}