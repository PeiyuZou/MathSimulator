//------------------------------------------------------------
// 描述：几何：点
// 作者：Z.P.Y
// 时间：2024/11/20 03:46
//------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Geometry
{
    /// <summary>
    /// 几何：点
    /// </summary>
    public class Point : Geometry
    {
        public float DisplayRadius = 0.1f;

        public Vector2 Position => new Vector2(transform.position.x, transform.position.z);

        private void OnDrawGizmos()
        {
            Handles.color = NormalColor;
            Handles.DrawSolidDisc(transform.position, Vector3.up, DisplayRadius);
        }

        public override bool IntersectWith(IIntersection intersection)
        {
            return false;
        }
    }
}