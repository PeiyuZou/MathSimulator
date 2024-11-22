//------------------------------------------------------------
// 描述：扇形
// 作者：Z.P.Y
// 时间：2024/11/20 10:37
//------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Geometry
{
    /// <summary>
    /// 扇形
    /// </summary>
    public class Sector : Geometry
    {
        public float Radius = 1f;

        public float Angle = 120f;

        public Vector2 Center => new Vector2(transform.position.x, transform.position.z);

        public Vector2 Direction => new Vector2(transform.forward.x, transform.forward.z);

        public float DirectionAngle => Vector2.SignedAngle(Vector2.right, Direction);

        public float StartAngle => DirectionAngle - Angle * 0.5f;

        public float EndAngle => DirectionAngle + Angle * 0.5f;

        public Vector2 StartArcPoint
        {
            get
            {
                var vec3 = Quaternion.Euler(0, -StartAngle, 0) * Vector3.right;
                return new Vector2(vec3.x, vec3.z) * Radius + Center;
            }
        }

        public Vector2 EndArcPoint
        {
            get
            {
                var vec3 = Quaternion.Euler(0, -EndAngle, 0) * Vector3.right;
                return new Vector2(vec3.x, vec3.z) * Radius + Center;
            }
        }

        private void OnDrawGizmos()
        {
            Handles.color = IntersectionTest && IntersectWith(IntersectionTarget) ? IntersectColor : NormalColor;

            // forward of the sector
            var forward = transform.forward;

            // left border of the sector
            var fromDirection = Quaternion.Euler(0, -Angle * 0.5f, 0) * forward;

            // draw the sector
            Handles.DrawSolidArc(transform.position, Vector3.up, fromDirection, Angle, Radius);
        }

        public override bool IntersectWith(IIntersection intersection)
        {
            if (intersection is Point point)
            {
                return IntersectionUtils.IsIntersect(this, point);
            }
            if (intersection is Rectangle rectangle)
            {
                return IntersectionUtils.IsIntersect(rectangle, this);
            }
            return false;
        }


    }
}