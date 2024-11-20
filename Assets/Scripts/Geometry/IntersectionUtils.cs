//------------------------------------------------------------
// 描述：几何的工具类
// 作者：Z.P.Y
// 时间：2024/11/20 02:07
//------------------------------------------------------------

using UnityEngine;

namespace Geometry
{
    /// <summary>
    /// 几何的工具类
    /// </summary>
    public static class IntersectionUtils
    {
        public static bool IsIntersect(Sector sector, Point point)
        {
            var pointVector = point.Position - sector.Center;
            if (pointVector.magnitude > sector.Radius)
            {
                return false;
            }
            var pointAngle = Vector2.SignedAngle(Vector2.right, pointVector);
            return pointAngle >= sector.StartAngle && pointAngle <= sector.EndAngle;
        }

        public static bool IsIntersect(Rectangle rectangle, Sector sector)
        {
            return false;
        }
    }
}