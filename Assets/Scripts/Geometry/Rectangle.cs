//------------------------------------------------------------
// 描述：矩形
// 作者：Z.P.Y
// 时间：2024/11/20 10:24
//------------------------------------------------------------

using System;
using UnityEditor;
using UnityEngine;

namespace Geometry
{
    /// <summary>
    /// 矩形
    /// </summary>
    public class Rectangle : MonoBehaviour, IIntersection
    {
        public Vector2 Size = Vector2.one;

        public Color NormalColor = new Color(0, 1f, 0, 0.5f);

        public Color IntersectColor = new Color(1, 0, 0, 0.5f);

        public bool IntersectionTest;

        public GameObject IntersectionGo;

        public IIntersection IntersectionTarget => !IntersectionGo ? null : IntersectionGo.GetComponent<IIntersection>();

        private readonly Vector3[] m_Vertices = new Vector3[4];

        public Vector2[] Vertices
        {
            get
            {
                const int len = 4;
                var vertices = new Vector2[len];
                for (var i = 0; i < len; ++i)
                {
                    vertices[i] = new Vector2(m_Vertices[i].x, m_Vertices[i].z);
                }
                return vertices;
            }
        }

        public Vector2 Center => new Vector2(transform.position.x, transform.position.z);

        private void OnDrawGizmos()
        {
            // cache
            var position = transform.position;
            var rotation = transform.rotation;

            // four vertices
            var topLeft = new Vector3(-Size.x * 0.5f, 0, Size.y * 0.5f);
            var topRight = new Vector3(Size.x * 0.5f, 0, Size.y * 0.5f);
            var bottomLeft = new Vector3(-Size.x * 0.5f, 0, -Size.y * 0.5f);
            var bottomRight = new Vector3(Size.x * 0.5f, 0, -Size.y * 0.5f);

            // take rotation
            m_Vertices[0] = position + rotation * topLeft;
            m_Vertices[1] = position + rotation * topRight;
            m_Vertices[2] = position + rotation * bottomRight;
            m_Vertices[3] = position + rotation * bottomLeft;

            var color = IntersectionTest && IntersectWith(IntersectionTarget) ? IntersectColor : NormalColor;
            // draw
            Handles.DrawSolidRectangleWithOutline(m_Vertices, color, color);
        }

        public bool IntersectWith(IIntersection intersection)
        {
            if (intersection is Point point)
            {
                return IntersectionUtils.IsIntersect(this, point);
            }
            return false;
        }
    }
}