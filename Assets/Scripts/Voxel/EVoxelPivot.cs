namespace Voxel
{
    /// <summary>
    /// 体素轴心
    /// </summary>
    public enum EVoxelPivot
    {
        /// <summary>
        /// 中心点
        /// </summary>
        CenterBottom = 0,
        /// <summary>
        /// 左下后
        /// </summary>
        VertexLdb = 1,
        /// <summary>
        /// 右下后
        /// </summary>
        VertexRdb = 2,
        /// <summary>
        /// 右下前
        /// </summary>
        VertexRdf = 3,
        /// <summary>
        /// 左下前
        /// </summary>
        VertexLdf = 4,
    }
}