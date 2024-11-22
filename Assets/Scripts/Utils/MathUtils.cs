//------------------------------------------------------------
// 描述：数学相关的算法
// 作者：Z.P.Y
// 时间：2024/11/21 09:01
//------------------------------------------------------------

using UnityEngine;

namespace Utils
{
    /// <summary>
    /// 数学相关的算法
    /// </summary>
    public static class MathUtils
    {
        /// <summary>
        /// 获取一个线段的斜率和截距
        /// </summary>
        /// <param name="p1">线段端点1</param>
        /// <param name="p2">线段端点2</param>
        /// <param name="slope">斜率</param>
        /// <param name="intercept">截距</param>
        public static void GetSlopeAndInterceptOfLine(Vector2 p1, Vector2 p2, out float slope, out float intercept)
        {
            slope = (p1.y - p2.y) / (p1.x - p2.x);
            intercept = p1.y - slope * p1.x;
        }
    }
}