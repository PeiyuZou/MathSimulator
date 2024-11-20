//------------------------------------------------------------
// 描述：向量扩展类
// 作者：Z.P.Y
// 时间：2024/11/06 07:21
//------------------------------------------------------------

using UnityEngine;

namespace Utils
{
    /// <summary>
    /// 向量扩展类
    /// </summary>
    public static class VectorExtend
    {
        public static Vector3Int ToVector3Int(this Vector3 vector)
        {
            return new Vector3Int(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y), Mathf.FloorToInt(vector.z));
        }
    }
}