//------------------------------------------------------------
// 描述：几何的工具类
// 作者：Z.P.Y
// 时间：2024/11/20 02:07
//------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Geometry
{
    /// <summary>
    /// 几何的工具类
    /// </summary>
    public static class IntersectionUtils
    {
        /// <summary>
        /// 指定两个线段是否相交
        /// </summary>
        /// <param name="start1"></param>
        /// <param name="end1"></param>
        /// <param name="start2"></param>
        /// <param name="end2"></param>
        /// <returns></returns>
        public static bool IsIntersect(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2)
        {
            // 计算线段方向向量
            var d1 = end1 - start1; // 线段1的方向
            var d2 = end2 - start2; // 线段2的方向

            // 计算叉积
            var cross = d1.Cross(d2);

            // 如果叉积接近于0，说明线段平行或共线
            if (Mathf.Abs(cross) < 1e-6f)
            {
                return false; // 平行线段不相交
            }

            // 计算交点的比例参数 t1 和 t2
            var diff = start2 - start1;
            var t1 = diff.Cross(d2) / cross; // 第一条线段上的比例参数
            var t2 = diff.Cross(d1) / cross; // 第二条线段上的比例参数

            // 判断交点是否在两条线段的范围内
            return t1 is >= 0 and <= 1 && t2 is >= 0 and <= 1;
        }

        /// <summary>
        /// 扇形和某坐标是否相交
        /// </summary>
        /// <param name="sector"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool IsIntersect(Sector sector, Vector2 point)
        {
            var pointVector = point - sector.Center;
            if (pointVector.magnitude > sector.Radius)
            {
                return false;
            }
            var pointAngle = Vector2.SignedAngle(Vector2.right, pointVector);
            return pointAngle >= sector.StartAngle && pointAngle <= sector.EndAngle;
        }

        /// <summary>
        /// 扇形和点是否相交
        /// </summary>
        /// <param name="sector"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool IsIntersect(Sector sector, Point point)
        {
            return IsIntersect(sector, point.Position);
        }

        /// <summary>
        /// 扇形和矩形是否相交
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="sector"></param>
        /// <returns></returns>
        public static bool IsIntersect(Rectangle rectangle, Sector sector)
        {
            var startPoint = sector.StartArcPoint;
            var endPoint = sector.EndArcPoint;

            var rectVertices = rectangle.Vertices;

            // 矩形顶点是否在扇形范围内
            foreach (var vertex in rectVertices)
            {
                if (IsIntersect(sector, vertex))
                {
                    return true;
                }
            }

            // 扇形顶点是否在矩形内
            if (IsIntersect(rectangle, startPoint) || IsIntersect(rectangle, endPoint) ||
                IsIntersect(rectangle, sector.Center))
            {
                return true;
            }

            // 扇形边与矩形边是否相交
            Vector2[] sectorEdges = { startPoint, endPoint, sector.Center };
            for (var i = 0; i < sectorEdges.Length; i++)
            {
                var start = sectorEdges[i];
                var end = sectorEdges[(i + 1) % sectorEdges.Length];
                for (var j = 0; j < rectVertices.Length; j++)
                {
                    var rectStart = rectVertices[j];
                    var rectEnd = rectVertices[(j + 1) % rectVertices.Length];
                    if (IsIntersect(start, end, rectStart, rectEnd))
                    {
                        return true;
                    }
                }
            }

            // 检查扇形弧线是否与矩形边相交
            for (var j = 0; j < rectVertices.Length; j++)
            {
                var rectStart = rectVertices[j];
                var rectEnd = rectVertices[(j + 1) % rectVertices.Length];
                if (IsArcIntersectingLine(sector, rectStart, rectEnd))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 线段和扇形是否相交
        /// </summary>
        /// <param name="sector"></param>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private static bool IsArcIntersectingLine(Sector sector, Vector2 startPoint, Vector2 endPoint)
        {
            // 直线公式：y = s * x + i
            MathUtils.GetSlopeAndInterceptOfLine(startPoint, endPoint, out var s, out var i);

            // 圆公式：(x - h) * (x - h) + (y - k) * (y - k) = r * r
            var h = sector.Center.x;
            var k = sector.Center.y;
            var r = sector.Radius;

            // 代入求交点，得到(s * s + 1) * x * x + (-2 * h + 2 * s * (i - k)) * x + (h * h + (i - k) * (i - k) - r * r) = 0
            // 对应a * x * x + b * x + c = 0, 求x
            var a = s * s + 1;
            var b = -2 * h + 2 * s * (i - k);
            var c = h * h + (i - k) * (i - k) - r * r;

            // x = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a) 或者 (-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a)
            // 设tri = b * b - 4 * a * c
            var tri = b * b - 4 * a * c;
            if (tri < 0) // 整个直线以及它的延长线都没有和圆相交
            {
                return false;
            }
            var intersectPoints = new List<Vector2>();
            if (tri == 0) // 整个直线以及它的延长线和圆仅有一个交点（相切）
            {
                var x = -b / (2 * a);
                var y = s * x + i;
                intersectPoints.Add(new Vector2(x, y));
            }
            if (tri > 0) // 整个直线以及它的延长线和圆有两个交点（相交）
            {
                var sqrt = Mathf.Sqrt(tri);
                // 相交点1
                var x = (-b + sqrt) / (2 * a);
                var y = s * x + i;
                intersectPoints.Add(new Vector2(x, y));
                // 相交点2
                x = (-b - sqrt) / (2 * a);
                y = s * x + i;
                intersectPoints.Add(new Vector2(x, y));
            }

            // 线段范围
            var minX = Mathf.Min(startPoint.x, endPoint.x);
            var maxX = Mathf.Max(startPoint.x, endPoint.x);
            var minY = Mathf.Min(startPoint.y, endPoint.y);
            var maxY = Mathf.Max(startPoint.y, endPoint.y);

            foreach (var intersectPoint in intersectPoints)
            {
                // 先排除不在线段内的交点
                if (intersectPoint.x < minX || intersectPoint.x > maxX || intersectPoint.y < minY ||
                    intersectPoint.y > maxY)
                {
                    continue;
                }
                var offset = intersectPoint - sector.Center;
                var angle = Vector2.SignedAngle(Vector2.right, offset);
                // 交点在扇形角度范围内，则相交
                if (angle >= sector.StartAngle && angle <= sector.EndAngle)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 矩形和某坐标是否相交
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool IsIntersect(Rectangle rectangle, Vector2 point)
        {
            var vertices = rectangle.Vertices;
            var topLeft = vertices[0];
            var topRight = vertices[1];
            var bottomRight = vertices[2];
            var bottomLeft = vertices[3];
            // 检查点是否在矩形四条边的同一侧
            return IsPointOnSameSide(point, topLeft, topRight, bottomRight) &&
                   IsPointOnSameSide(point, topRight, bottomRight, bottomLeft) &&
                   IsPointOnSameSide(point, bottomRight, bottomLeft, topLeft) &&
                   IsPointOnSameSide(point, bottomLeft, topLeft, topRight);
        }

        /// <summary>
        /// 矩形和点是否相交
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool IsIntersect(Rectangle rectangle, Point point)
        {
            return IsIntersect(rectangle, point.Position);
        }

        // 判断点P是否在由三点定义的边的同一侧
        private static bool IsPointOnSameSide(Vector2 p, Vector2 a, Vector2 b, Vector2 c)
        {
            var ab = b - a;
            var ac = c - a;
            var ap = p - a;

            // 计算叉积，检查点P是否在AB与AC形成的平面同一侧
            var cross1 = ab.Cross(ac);
            var cross2 = ab.Cross(ap);

            // 如果叉积符号相同，点P在相同的侧面
            return Mathf.Approximately(Mathf.Sign(cross1), Mathf.Sign(cross2)) || cross2 == 0;
        }
    }
}