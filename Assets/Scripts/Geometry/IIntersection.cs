namespace Geometry
{
    /// <summary>
    /// 接口化：相交
    /// </summary>
    public interface IIntersection
    {
        public IIntersection IntersectionTarget { get; }

        bool IntersectWith(IIntersection intersection);
    }
}