//------------------------------------------------------------
// 描述：几何
// 作者：Z.P.Y
// 时间：2024/11/22 02:04
//------------------------------------------------------------

using UnityEngine;

namespace Geometry
{
    /// <summary>
    /// 几何
    /// </summary>
    public abstract class Geometry : MonoBehaviour, IIntersection
    {
        public Color NormalColor = new Color(0, 1f, 0, 0.5f);

        public Color IntersectColor = new Color(1f, 0, 0, 0.5f);

        public bool IntersectionTest;

        public GameObject IntersectionGo;

        public IIntersection IntersectionTarget => !IntersectionGo ? null : IntersectionGo.GetComponent<IIntersection>();

        public abstract bool IntersectWith(IIntersection intersection);
    }
}