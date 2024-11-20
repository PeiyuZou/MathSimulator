using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Axis
{
    public class AxisPrompt : MonoBehaviour
    {
        public float ArrowSize = 1f;
        public bool ShowValue = true;
        public float UnitValue = 1f;
        [Range(0, 100)]
        public int ValueSize = 50;

        [Header("X Axis")]
        public Vector2 XAxisRange = new Vector2(0, 10f);
        public string XAxisLabel = "X";
        public Color XAxisColor = Color.red;
        private readonly Vector3 m_XAxisDirection = Vector3.right;
        private const int X_AXIS_ID = 0;

        [Header("Y Axis")]
        public Vector2 YAxisRange = new Vector2(0f, 10f);
        public string YAxisLabel = "Y";
        public Color YAxisColor = Color.green;
        private readonly Vector3 m_YAxisDirection = Vector3.up;
        private const int Y_AXIS_ID = 1;

        [Header("Z Axis")]
        public Vector2 ZAxisRange = new Vector2(0f, 10f);
        public string ZAxisLabel = "Z";
        public Color ZAxisColor = Color.blue;
        private readonly Vector3 m_ZAxisDirection = Vector3.forward;
        private const int Z_AXIS_ID = 2;

        [Header("Custom Point List")]
        public List<Vector3> Points = new List<Vector3>();

        // private fields
        private readonly CultureInfo m_ValueCulture = CultureInfo.InvariantCulture;
        private const EventType REPAINT = EventType.Repaint;
        private readonly Color m_White = Color.white;
        private Vector3 m_TempPos;
        private Quaternion m_TempQua;

        private void Awake()
        {
            Debug.LogError(new Vector3(3, 2, 4).Compress_20());
        }

        private void OnDrawGizmos()
        {
            if (ShowValue)
            {
                m_TempPos.x = 0;
                m_TempPos.y = 0;
                m_TempPos.z = 0;
                Handles.Label(m_TempPos, m_TempPos.x.ToString(m_ValueCulture));
            }

            // 绘制X轴
            Handles.color = XAxisColor;
            Handles.DrawLine(m_XAxisDirection * XAxisRange.x, m_XAxisDirection * XAxisRange.y);
            m_TempPos = m_XAxisDirection * (XAxisRange.y - ArrowSize);
            m_TempQua = Quaternion.LookRotation(Vector3.right);
            Handles.ArrowHandleCap(X_AXIS_ID, m_TempPos, m_TempQua, ArrowSize, REPAINT);
            m_TempPos = m_XAxisDirection * XAxisRange.y;
            Handles.Label(m_TempPos, XAxisLabel);
            // 绘制X轴坐标
            if (ShowValue)
            {
                for (var i = 1; i <= ValueSize; ++i)
                {
                    m_TempPos = m_XAxisDirection * UnitValue * i;
                    if (m_TempPos.x < XAxisRange.y)
                    {
                        Handles.Label(m_TempPos, m_TempPos.x.ToString(m_ValueCulture));
                    }
                    m_TempPos = -m_TempPos;
                    if (m_TempPos.x > XAxisRange.x)
                    {
                        Handles.Label(m_TempPos, m_TempPos.x.ToString(m_ValueCulture));
                    }
                }
            }

            // 绘制Y轴
            Handles.color = YAxisColor;
            Handles.DrawLine(m_YAxisDirection * YAxisRange.x, m_YAxisDirection * YAxisRange.y);
            m_TempPos = m_YAxisDirection * (YAxisRange.y - ArrowSize);
            m_TempQua = Quaternion.LookRotation(Vector3.up);
            Handles.ArrowHandleCap(Y_AXIS_ID, m_TempPos, m_TempQua, ArrowSize, REPAINT);
            m_TempPos = m_YAxisDirection * YAxisRange.y;
            Handles.Label(m_TempPos, YAxisLabel);
            // 绘制Y轴坐标
            if (ShowValue)
            {
                for (var i = 1; i <= ValueSize; ++i)
                {
                    m_TempPos = m_YAxisDirection * UnitValue * i;
                    if (m_TempPos.y < YAxisRange.y)
                    {
                        Handles.Label(m_TempPos, m_TempPos.y.ToString(m_ValueCulture));
                    }
                    m_TempPos = -m_TempPos;
                    if (m_TempPos.y > YAxisRange.x)
                    {
                        Handles.Label(m_TempPos, m_TempPos.y.ToString(m_ValueCulture));
                    }
                }
            }

            // 绘制Z轴
            Handles.color = ZAxisColor;
            Handles.DrawLine(m_ZAxisDirection * ZAxisRange.x, m_ZAxisDirection * ZAxisRange.y);
            m_TempPos = m_ZAxisDirection * (ZAxisRange.y - ArrowSize);
            m_TempQua = Quaternion.LookRotation(Vector3.forward);
            Handles.ArrowHandleCap(Z_AXIS_ID, m_TempPos, m_TempQua, ArrowSize, REPAINT);
            m_TempPos = m_ZAxisDirection * ZAxisRange.y;
            Handles.Label(m_TempPos, ZAxisLabel);
            // 绘制Z轴坐标
            if (ShowValue)
            {
                for (var i = 1; i <= ValueSize; ++i)
                {
                    m_TempPos = m_ZAxisDirection * UnitValue * i;
                    if (m_TempPos.z < ZAxisRange.y)
                    {
                        Handles.Label(m_TempPos, m_TempPos.z.ToString(m_ValueCulture));
                    }
                    m_TempPos = -m_TempPos;
                    if (m_TempPos.z > ZAxisRange.x)
                    {
                        Handles.Label(m_TempPos, m_TempPos.z.ToString(m_ValueCulture));
                    }
                }
            }

            Handles.color = m_White;

            foreach (var pos in Points)
            {
                Handles.Label(pos, pos.ToString());
            }
        }

        private void OnValidate()
        {
            if (ArrowSize < 0)
            {
                ArrowSize = 0;
            }
            if (ArrowSize > 100)
            {
                ArrowSize = 100;
            }

            if (UnitValue < 0)
            {
                UnitValue = 0;
            }

            if (XAxisRange.x > XAxisRange.y)
            {
                XAxisRange.x = XAxisRange.y;
            }
            if (XAxisRange.y < XAxisRange.x)
            {
                XAxisRange.y = XAxisRange.x;
            }
            if (YAxisRange.x > YAxisRange.y)
            {
                YAxisRange.x = YAxisRange.y;
            }
            if (YAxisRange.y < YAxisRange.x)
            {
                YAxisRange.y = YAxisRange.x;
            }
            if (ZAxisRange.x > ZAxisRange.y)
            {
                ZAxisRange.x = ZAxisRange.y;
            }
            if (ZAxisRange.y < ZAxisRange.x)
            {
                ZAxisRange.y = ZAxisRange.x;
            }
        }
    }
}
