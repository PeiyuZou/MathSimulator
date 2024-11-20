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

        public Color Color = new Color(0, 1f, 0, 0.5f);

        public readonly Vector3[] Vertices = new Vector3[4];

        private void OnDrawGizmos()
        {
            Handles.color = Color;

            // cache
            var position = transform.position;
            var rotation = transform.rotation;

            // four vertices
            var topLeft = new Vector3(-Size.x * 0.5f, 0, Size.y * 0.5f);
            var topRight = new Vector3(Size.x * 0.5f, 0, Size.y * 0.5f);
            var bottomLeft = new Vector3(-Size.x * 0.5f, 0, -Size.y * 0.5f);
            var bottomRight = new Vector3(Size.x * 0.5f, 0, -Size.y * 0.5f);

            // take rotation
            Vertices[0] = position + rotation * topLeft;
            Vertices[1] = position + rotation * topRight;
            Vertices[2] = position + rotation * bottomRight;
            Vertices[3] = position + rotation * bottomLeft;

            // draw
            Handles.DrawSolidRectangleWithOutline(Vertices, Color, Color);
        }

        public bool IntersectWith(IIntersection intersection)
        {
            return false;
        }
    }
}