//------------------------------------------------------------
// 描述：扇形
// 作者：Z.P.Y
// 时间：2024/11/20 10:37
//------------------------------------------------------------

using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Geometry
{
    /// <summary>
    /// 扇形
    /// </summary>
    public class Sector : MonoBehaviour, IIntersection
    {
        public float Radius = 1f;

        public float Angle = 120f;

        public Color NormalColor = new Color(0, 1f, 0, 0.5f);

        public Color IntersectColor = new Color(1f, 0, 0, 0.5f);

        public bool IntersectionTest;

        public GameObject IntersectionGo;

        public IIntersection IntersectionTarget => IntersectionGo?.GetComponent<IIntersection>();

        public Vector2 Center => new Vector2(transform.position.x, transform.position.z);

        public Vector2 Direction => new Vector2(transform.forward.x, transform.forward.z);

        public float DirectionAngle => Vector2.SignedAngle(Vector2.right, Direction);

        public float StartAngle => DirectionAngle - Angle * 0.5f;

        public float EndAngle => DirectionAngle + Angle * 0.5f;

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