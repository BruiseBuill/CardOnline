using UnityEngine;
using UnityEditor;

namespace CardOnline
{
    /// <summary>
    /// 卡牌排列系统的自定义编辑器
    /// </summary>
    [CustomEditor(typeof(CardAllignment))]
    public class CardAllignmentEditor : Editor
    {
        private CardAllignment cardAllignment;
        private bool showAdvancedSettings = false;
        
        void OnEnable()
        {
            cardAllignment = (CardAllignment)target;
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            // 绘制默认Inspector
            DrawDefaultInspector();
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("快速操作", EditorStyles.boldLabel);
            
            // 快速操作按钮
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("重新生成卡牌"))
            {
                cardAllignment.RegenerateCards();
            }
            if (GUILayout.Button("重置控制点"))
            {
                ResetControlPoints();
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("创建平滑弧线"))
            {
                CreateSmoothArc();
            }
            if (GUILayout.Button("创建S形曲线"))
            {
                CreateSShape();
            }
            EditorGUILayout.EndHorizontal();
            
            // 高级设置
            showAdvancedSettings = EditorGUILayout.Foldout(showAdvancedSettings, "高级设置");
            if (showAdvancedSettings)
            {
                EditorGUI.indentLevel++;
                DrawAdvancedSettings();
                EditorGUI.indentLevel--;
            }
            
            serializedObject.ApplyModifiedProperties();
        }
        
        private void DrawAdvancedSettings()
        {
            EditorGUILayout.LabelField("贝塞尔曲线信息", EditorStyles.boldLabel);
            
            if (cardAllignment.transform.childCount >= 4)
            {
                Transform startPoint = cardAllignment.transform.GetChild(0);
                Transform controlPoint1 = cardAllignment.transform.GetChild(1);
                Transform controlPoint2 = cardAllignment.transform.GetChild(2);
                Transform endPoint = cardAllignment.transform.GetChild(3);
                
                if (startPoint != null && controlPoint1 != null && controlPoint2 != null && endPoint != null)
                {
                    float curveLength = BezierCurveUtility.CalculateCubicBezierLength(
                        startPoint.localPosition,
                        controlPoint1.localPosition,
                        controlPoint2.localPosition,
                        endPoint.localPosition
                    );
                    
                    EditorGUILayout.LabelField($"曲线长度: {curveLength:F2}");
                    
                    float curvature = BezierCurveUtility.CalculateCurvature(
                        startPoint.localPosition,
                        controlPoint1.localPosition,
                        controlPoint2.localPosition,
                        endPoint.localPosition,
                        0.5f
                    );
                    
                    EditorGUILayout.LabelField($"中点曲率: {curvature:F4}");
                }
            }
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("预设曲线", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("直线"))
            {
                CreateStraightLine();
            }
            if (GUILayout.Button("半圆"))
            {
                CreateSemicircle();
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("波浪"))
            {
                CreateWave();
            }
            if (GUILayout.Button("心形"))
            {
                CreateHeart();
            }
            EditorGUILayout.EndHorizontal();
        }
        
        private void ResetControlPoints()
        {
            Transform startPoint = cardAllignment.transform.Find("StartPoint");
            Transform controlPoint1 = cardAllignment.transform.Find("ControlPoint1");
            Transform controlPoint2 = cardAllignment.transform.Find("ControlPoint2");
            Transform endPoint = cardAllignment.transform.Find("EndPoint");
            
            if (startPoint != null) startPoint.localPosition = new Vector3(-2f, 0f, 0f);
            if (controlPoint1 != null) controlPoint1.localPosition = new Vector3(-1f, 2f, 0f);
            if (controlPoint2 != null) controlPoint2.localPosition = new Vector3(1f, 2f, 0f);
            if (endPoint != null) endPoint.localPosition = new Vector3(2f, 0f, 0f);
        }
        
        private void CreateSmoothArc()
        {
            Transform startPoint = cardAllignment.transform.Find("StartPoint");
            Transform endPoint = cardAllignment.transform.Find("EndPoint");
            
            if (startPoint != null && endPoint != null)
            {
                Vector3[] controlPoints = BezierCurveUtility.CreateSmoothControlPoints(
                    startPoint.localPosition,
                    endPoint.localPosition,
                    1.5f,
                    1
                );
                
                Transform controlPoint1 = cardAllignment.transform.Find("ControlPoint1");
                Transform controlPoint2 = cardAllignment.transform.Find("ControlPoint2");
                
                if (controlPoint1 != null) controlPoint1.localPosition = controlPoints[0];
                if (controlPoint2 != null) controlPoint2.localPosition = controlPoints[1];
            }
        }
        
        private void CreateSShape()
        {
            Transform startPoint = cardAllignment.transform.Find("StartPoint");
            Transform controlPoint1 = cardAllignment.transform.Find("ControlPoint1");
            Transform controlPoint2 = cardAllignment.transform.Find("ControlPoint2");
            Transform endPoint = cardAllignment.transform.Find("EndPoint");
            
            if (startPoint != null) startPoint.localPosition = new Vector3(-2f, 0f, 0f);
            if (controlPoint1 != null) controlPoint1.localPosition = new Vector3(-1f, 1.5f, 0f);
            if (controlPoint2 != null) controlPoint2.localPosition = new Vector3(1f, -1.5f, 0f);
            if (endPoint != null) endPoint.localPosition = new Vector3(2f, 0f, 0f);
        }
        
        private void CreateStraightLine()
        {
            Transform startPoint = cardAllignment.transform.Find("StartPoint");
            Transform controlPoint1 = cardAllignment.transform.Find("ControlPoint1");
            Transform controlPoint2 = cardAllignment.transform.Find("ControlPoint2");
            Transform endPoint = cardAllignment.transform.Find("EndPoint");
            
            if (startPoint != null) startPoint.localPosition = new Vector3(-2f, 0f, 0f);
            if (controlPoint1 != null) controlPoint1.localPosition = new Vector3(-0.67f, 0f, 0f);
            if (controlPoint2 != null) controlPoint2.localPosition = new Vector3(0.67f, 0f, 0f);
            if (endPoint != null) endPoint.localPosition = new Vector3(2f, 0f, 0f);
        }
        
        private void CreateSemicircle()
        {
            Transform startPoint = cardAllignment.transform.Find("StartPoint");
            Transform controlPoint1 = cardAllignment.transform.Find("ControlPoint1");
            Transform controlPoint2 = cardAllignment.transform.Find("ControlPoint2");
            Transform endPoint = cardAllignment.transform.Find("EndPoint");
            
            if (startPoint != null) startPoint.localPosition = new Vector3(-2f, 0f, 0f);
            if (controlPoint1 != null) controlPoint1.localPosition = new Vector3(-2f, 2.5f, 0f);
            if (controlPoint2 != null) controlPoint2.localPosition = new Vector3(2f, 2.5f, 0f);
            if (endPoint != null) endPoint.localPosition = new Vector3(2f, 0f, 0f);
        }
        
        private void CreateWave()
        {
            Transform startPoint = cardAllignment.transform.Find("StartPoint");
            Transform controlPoint1 = cardAllignment.transform.Find("ControlPoint1");
            Transform controlPoint2 = cardAllignment.transform.Find("ControlPoint2");
            Transform endPoint = cardAllignment.transform.Find("EndPoint");
            
            if (startPoint != null) startPoint.localPosition = new Vector3(-2f, 0f, 0f);
            if (controlPoint1 != null) controlPoint1.localPosition = new Vector3(-1f, 1f, 0f);
            if (controlPoint2 != null) controlPoint2.localPosition = new Vector3(1f, -1f, 0f);
            if (endPoint != null) endPoint.localPosition = new Vector3(2f, 0f, 0f);
        }
        
        private void CreateHeart()
        {
            Transform startPoint = cardAllignment.transform.Find("StartPoint");
            Transform controlPoint1 = cardAllignment.transform.Find("ControlPoint1");
            Transform controlPoint2 = cardAllignment.transform.Find("ControlPoint2");
            Transform endPoint = cardAllignment.transform.Find("EndPoint");
            
            if (startPoint != null) startPoint.localPosition = new Vector3(-1.5f, 0f, 0f);
            if (controlPoint1 != null) controlPoint1.localPosition = new Vector3(-1.5f, 2f, 0f);
            if (controlPoint2 != null) controlPoint2.localPosition = new Vector3(1.5f, 2f, 0f);
            if (endPoint != null) endPoint.localPosition = new Vector3(1.5f, 0f, 0f);
        }
        
        void OnSceneGUI()
        {
            if (cardAllignment == null) return;
            
            // 绘制控制点的自定义手柄
            DrawControlPointHandles();
        }
        
        private void DrawControlPointHandles()
        {
            Transform startPoint = cardAllignment.transform.Find("StartPoint");
            Transform controlPoint1 = cardAllignment.transform.Find("ControlPoint1");
            Transform controlPoint2 = cardAllignment.transform.Find("ControlPoint2");
            Transform endPoint = cardAllignment.transform.Find("EndPoint");
            
            if (startPoint != null)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 newPosition = Handles.PositionHandle(startPoint.position, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(startPoint, "Move Start Point");
                    startPoint.position = newPosition;
                }
            }
            
            if (controlPoint1 != null)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 newPosition = Handles.PositionHandle(controlPoint1.position, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(controlPoint1, "Move Control Point 1");
                    controlPoint1.position = newPosition;
                }
            }
            
            if (controlPoint2 != null)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 newPosition = Handles.PositionHandle(controlPoint2.position, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(controlPoint2, "Move Control Point 2");
                    controlPoint2.position = newPosition;
                }
            }
            
            if (endPoint != null)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 newPosition = Handles.PositionHandle(endPoint.position, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(endPoint, "Move End Point");
                    endPoint.position = newPosition;
                }
            }
        }
    }
}
