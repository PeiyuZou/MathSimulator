//------------------------------------------------------------
// 描述：线段
// 作者：Z.P.Y
// 时间：2024/11/22 02:02
//------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Geometry
{
    /// <summary>
    /// 线段
    /// </summary>
    public class Segment : Geometry
    {
        public Vector2 RelativeStartPoint;

        public Vector2 RelativeEndPoint = Vector2.right;

        public float Thickness = 3f;

        public float PointRadius = 0.1f;

        public Vector2 Center => new Vector2(transform.position.x, transform.position.z);

        public Vector2 StartPoint => Center + RelativeStartPoint;

        public Vector2 EndPoint => Center + RelativeEndPoint;

        private readonly Color m_PointColor = new Color(0f, 0f, 0f, 0.5f);

        private Vector3 m_DrawStart;

        private Vector3 m_DrawEnd;

        private void OnDrawGizmos()
        {
            Handles.color = IntersectionTest && IntersectWith(IntersectionTarget) ? IntersectColor : NormalColor;
            var start = StartPoint;
            var end = EndPoint;
            m_DrawStart.x = start.x;
            m_DrawStart.z = start.y;
            m_DrawEnd.x = end.x;
            m_DrawEnd.z = end.y;
            Handles.DrawLine(m_DrawStart, m_DrawEnd, Thickness);
            Handles.color = m_PointColor;
            Handles.DrawSolidDisc(m_DrawStart, Vector3.up, PointRadius);
            Handles.DrawSolidDisc(m_DrawEnd, Vector3.up, PointRadius);
        }

        public override bool IntersectWith(IIntersection intersection)
        {
            if (intersection is Segment segment)
            {
                return IntersectionUtils.IsIntersect(this, segment);
            }
            return false;
        }
    }
}