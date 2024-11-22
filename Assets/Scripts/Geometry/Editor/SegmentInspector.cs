//------------------------------------------------------------
// 描述：Segment Inspector Extend
// 作者：Z.P.Y
// 时间：2024/11/22 02:34
//------------------------------------------------------------

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Geometry.Editor
{
    /// <summary>
    /// Segment Inspector Extend
    /// </summary>
    [CustomEditor(typeof(Segment))]
    public class SegmentInspector : GeometryInspector
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            root.Bind(serializedObject);

            var startPointField = new Vector2Field("Relative Start Point")
            {
                bindingPath = "RelativeStartPoint",
            };
            root.Add(startPointField);

            var endPointField = new Vector2Field("Relative End Point")
            {
                bindingPath = "RelativeEndPoint",
            };
            root.Add(endPointField);

            var thicknessField = new FloatField("Thickness")
            {
                bindingPath = "Thickness",
            };
            root.Add(thicknessField);

            var pointRadius = new FloatField("Point Radius")
            {
                bindingPath = "PointRadius",
            };
            root.Add(pointRadius);

            root.Add(GetCommonElement());

            return root;
        }
    }
}