using System.Collections.Generic;
using UnityEngine;

namespace CardOnline
{
    /// <summary>
    /// 贝塞尔曲线工具类 - 提供各种贝塞尔曲线计算功能
    /// </summary>
    public static class BezierCurveUtility
    {
        /// <summary>
        /// 计算三次贝塞尔曲线上的点
        /// </summary>
        /// <param name="p0">起始点</param>
        /// <param name="p1">控制点1</param>
        /// <param name="p2">控制点2</param>
        /// <param name="p3">结束点</param>
        /// <param name="t">参数t (0-1)</param>
        /// <returns>曲线上的点</returns>
        public static Vector3 CalculateCubicBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
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
        /// 计算二次贝塞尔曲线上的点
        /// </summary>
        /// <param name="p0">起始点</param>
        /// <param name="p1">控制点</param>
        /// <param name="p2">结束点</param>
        /// <param name="t">参数t (0-1)</param>
        /// <returns>曲线上的点</returns>
        public static Vector3 CalculateQuadraticBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            
            Vector3 point = uu * p0;
            point += 2 * u * t * p1;
            point += tt * p2;
            
            return point;
        }
        
        /// <summary>
        /// 计算三次贝塞尔曲线的切线方向
        /// </summary>
        /// <param name="p0">起始点</param>
        /// <param name="p1">控制点1</param>
        /// <param name="p2">控制点2</param>
        /// <param name="p3">结束点</param>
        /// <param name="t">参数t (0-1)</param>
        /// <returns>切线方向向量</returns>
        public static Vector3 CalculateCubicBezierTangent(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            
            Vector3 tangent = 3 * uu * (p1 - p0);
            tangent += 6 * u * t * (p2 - p1);
            tangent += 3 * tt * (p3 - p2);
            
            return tangent.normalized;
        }
        
        /// <summary>
        /// 计算二次贝塞尔曲线的切线方向
        /// </summary>
        /// <param name="p0">起始点</param>
        /// <param name="p1">控制点</param>
        /// <param name="p2">结束点</param>
        /// <param name="t">参数t (0-1)</param>
        /// <returns>切线方向向量</returns>
        public static Vector3 CalculateQuadraticBezierTangent(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            Vector3 tangent = 2 * (1 - t) * (p1 - p0) + 2 * t * (p2 - p1);
            return tangent.normalized;
        }
        
        /// <summary>
        /// 计算贝塞尔曲线的长度（近似）
        /// </summary>
        /// <param name="p0">起始点</param>
        /// <param name="p1">控制点1</param>
        /// <param name="p2">控制点2</param>
        /// <param name="p3">结束点</param>
        /// <param name="segments">分段数量</param>
        /// <returns>曲线长度</returns>
        public static float CalculateCubicBezierLength(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, int segments = 50)
        {
            float length = 0f;
            Vector3 previousPoint = CalculateCubicBezierPoint(p0, p1, p2, p3, 0);
            
            for (int i = 1; i <= segments; i++)
            {
                float t = (float)i / segments;
                Vector3 currentPoint = CalculateCubicBezierPoint(p0, p1, p2, p3, t);
                length += Vector3.Distance(previousPoint, currentPoint);
                previousPoint = currentPoint;
            }
            
            return length;
        }
        
        /// <summary>
        /// 根据弧长参数化贝塞尔曲线
        /// </summary>
        /// <param name="p0">起始点</param>
        /// <param name="p1">控制点1</param>
        /// <param name="p2">控制点2</param>
        /// <param name="p3">结束点</param>
        /// <param name="arcLength">目标弧长</param>
        /// <param name="segments">分段数量</param>
        /// <returns>对应的t值</returns>
        public static float ArcLengthToT(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float arcLength, int segments = 100)
        {
            float totalLength = CalculateCubicBezierLength(p0, p1, p2, p3, segments);
            float targetLength = arcLength * totalLength;
            
            float currentLength = 0f;
            Vector3 previousPoint = CalculateCubicBezierPoint(p0, p1, p2, p3, 0);
            
            for (int i = 1; i <= segments; i++)
            {
                float t = (float)i / segments;
                Vector3 currentPoint = CalculateCubicBezierPoint(p0, p1, p2, p3, t);
                float segmentLength = Vector3.Distance(previousPoint, currentPoint);
                
                if (currentLength + segmentLength >= targetLength)
                {
                    // 线性插值找到精确的t值
                    float remainingLength = targetLength - currentLength;
                    float tRatio = remainingLength / segmentLength;
                    return (i - 1) / (float)segments + tRatio / segments;
                }
                
                currentLength += segmentLength;
                previousPoint = currentPoint;
            }
            
            return 1f;
        }
        
        /// <summary>
        /// 生成贝塞尔曲线上的点列表
        /// </summary>
        /// <param name="p0">起始点</param>
        /// <param name="p1">控制点1</param>
        /// <param name="p2">控制点2</param>
        /// <param name="p3">结束点</param>
        /// <param name="resolution">分辨率</param>
        /// <returns>曲线上的点列表</returns>
        public static List<Vector3> GenerateBezierPoints(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, int resolution = 50)
        {
            List<Vector3> points = new List<Vector3>();
            
            for (int i = 0; i <= resolution; i++)
            {
                float t = (float)i / resolution;
                Vector3 point = CalculateCubicBezierPoint(p0, p1, p2, p3, t);
                points.Add(point);
            }
            
            return points;
        }
        
        /// <summary>
        /// 计算贝塞尔曲线的曲率
        /// </summary>
        /// <param name="p0">起始点</param>
        /// <param name="p1">控制点1</param>
        /// <param name="p2">控制点2</param>
        /// <param name="p3">结束点</param>
        /// <param name="t">参数t (0-1)</param>
        /// <returns>曲率值</returns>
        public static float CalculateCurvature(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            // 计算一阶导数（切线）
            Vector3 firstDerivative = CalculateCubicBezierTangent(p0, p1, p2, p3, t);
            
            // 计算二阶导数
            float u = 1 - t;
            Vector3 secondDerivative = 6 * u * (p2 - 2 * p1 + p0) + 6 * t * (p3 - 2 * p2 + p1);
            
            // 计算曲率 κ = |v' × v''| / |v'|³
            Vector3 crossProduct = Vector3.Cross(firstDerivative, secondDerivative);
            float curvature = crossProduct.magnitude / Mathf.Pow(firstDerivative.magnitude, 3);
            
            return curvature;
        }
        
        /// <summary>
        /// 创建平滑的贝塞尔曲线控制点
        /// </summary>
        /// <param name="startPoint">起始点</param>
        /// <param name="endPoint">结束点</param>
        /// <param name="curveHeight">曲线高度</param>
        /// <param name="curveDirection">曲线方向（1为向上，-1为向下）</param>
        /// <returns>控制点数组 [controlPoint1, controlPoint2]</returns>
        public static Vector3[] CreateSmoothControlPoints(Vector3 startPoint, Vector3 endPoint, float curveHeight = 1f, int curveDirection = 1)
        {
            Vector3 direction = (endPoint - startPoint).normalized;
            Vector3 perpendicular = Vector3.Cross(direction, Vector3.forward) * curveDirection;
            
            Vector3 controlPoint1 = startPoint + direction * 0.33f + perpendicular * curveHeight;
            Vector3 controlPoint2 = endPoint - direction * 0.33f + perpendicular * curveHeight;
            
            return new Vector3[] { controlPoint1, controlPoint2 };
        }
        
        /// <summary>
        /// 计算贝塞尔曲线上的法向量
        /// </summary>
        /// <param name="p0">起始点</param>
        /// <param name="p1">控制点1</param>
        /// <param name="p2">控制点2</param>
        /// <param name="p3">结束点</param>
        /// <param name="t">参数t (0-1)</param>
        /// <returns>法向量</returns>
        public static Vector3 CalculateNormal(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            Vector3 tangent = CalculateCubicBezierTangent(p0, p1, p2, p3, t);
            Vector3 normal = Vector3.Cross(tangent, Vector3.forward);
            return normal.normalized;
        }
    }
}
