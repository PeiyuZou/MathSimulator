//------------------------------------------------------------
// 描述：坐标转换相关扩展类
// 作者：Z.P.Y
// 时间：2024/11/04 06:11
//------------------------------------------------------------

using UnityEngine;

namespace Utils
{
    /// <summary>
    /// 坐标转换相关扩展类
    /// </summary>
    public static class PosConvertExtend
    {
        private const int SCALE_10 = 10;

        private const float SCALE_10_REVERSE = 0.1f;

        private const int CHUNK_SIZE = 32;

        private const float CHUNK_SIZE_REVERSE = 0.03125f;

        #region Compress

        public static long Compress_20(this Vector3 vector3)
        {
            var cx = Compress_20Bit((long)System.Math.Floor(vector3.x)) << 40;
            var cy = Compress_20Bit((long)System.Math.Floor(vector3.y)) << 20;
            var cz = Compress_20Bit((long)System.Math.Floor(vector3.z));
            return cx | cy | cz;
        }

        public static long Compress_20x10(this Vector3 vector3)
        {
            var sx = Compress_20Bit((long)System.Math.Floor(vector3.x * SCALE_10)) << 40;
            var sy = Compress_20Bit((long)System.Math.Floor(vector3.y * SCALE_10)) << 20;
            var sz = Compress_20Bit((long)System.Math.Floor(vector3.z * SCALE_10));
            return sx | sy | sz;
        }

        private static long Compress_20Bit(long value)
        {
            if (value < 0)
            {
                return System.Math.Abs(value) | 0x80000;
            }
            return value;
        }

        #endregion

        #region Decompress

        public static Vector3 Decompress_20(this long pid)
        {
            var z = Decompress_20Bit(pid & 0xfffff);
            var y = Decompress_20Bit(pid >> 20 & 0xfffff);
            var x = Decompress_20Bit(pid >> 40 & 0xfffff);
            return new Vector3(x, y, z);
        }

        public static Vector3 Decompress_20x10(this long pid)
        {
            var z = Decompress_20Bit(pid & 0xfffff) * SCALE_10_REVERSE;
            var y = Decompress_20Bit(pid >> 20 & 0xfffff) * SCALE_10_REVERSE;
            var x = Decompress_20Bit(pid >> 40 & 0xfffff) * SCALE_10_REVERSE;
            return new Vector3(x, y, z);
        }

        private static long Decompress_20Bit(long value)
        {
            if ((value & 0x80000) > 1)
            {
                return (value & 0x7ffff) * -1;
            }
            return value;
        }

        #endregion

        #region Pos <--> Chunk Offset

        public static Vector3 PosToChunkOffset(this Vector3 vector3)
        {
            var x = Mathf.Floor(vector3.x * CHUNK_SIZE_REVERSE);
            var y = Mathf.Floor(vector3.y * CHUNK_SIZE_REVERSE);
            var z = Mathf.Floor(vector3.z * CHUNK_SIZE_REVERSE);
            return new Vector3(x, y, z);
        }

        public static Vector3 ChunkOffsetToPos(this Vector3 vector3)
        {
            var x = Mathf.Floor(vector3.x) * CHUNK_SIZE;
            var y = Mathf.Floor(vector3.y) * CHUNK_SIZE;
            var z = Mathf.Floor(vector3.z) * CHUNK_SIZE;
            return new Vector3(x, y, z);
        }

        #endregion

        #region Chunk Compress

        public static long PosToChunkId(this Vector3 vector3)
        {
            var chunkOffset = vector3.PosToChunkOffset();
            return chunkOffset.Compress_20();
        }

        public static Vector3 ChunkIdToPos(this long chunkId)
        {
            var chunkOffset = chunkId.Decompress_20();
            return chunkOffset.ChunkOffsetToPos();
        }

        #endregion
    }
}