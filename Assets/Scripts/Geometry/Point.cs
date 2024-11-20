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
    public class Point : MonoBehaviour, IIntersection
    {
        public float DisplayRadius = 0.2f;

        public Color NormalColor = new Color(0, 0, 1f, 0.5f);

        public Color IntersectColor = new Color(1, 0, 0f, 0.5f);

        public Vector2 Position => new Vector2(transform.position.x, transform.position.z);

        private void OnDrawGizmos()
        {
            Handles.color = NormalColor;
            Handles.DrawSolidDisc(transform.position, Vector3.up, DisplayRadius);
        }

        public bool IntersectWith(IIntersection intersection)
        {
            return false;
        }
    }
}