namespace Geometry
{
    /// <summary>
    /// 接口化：相交
    /// </summary>
    public interface IIntersection
    {
        bool IntersectWith(IIntersection intersection);
    }
}