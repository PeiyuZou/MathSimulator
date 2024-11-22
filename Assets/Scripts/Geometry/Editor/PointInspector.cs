//------------------------------------------------------------
// 描述：Point Inspector Extend
// 作者：Z.P.Y
// 时间：2024/11/20 04:02
//------------------------------------------------------------

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Geometry.Editor
{
    /// <summary>
    /// Point Inspector Extend
    /// </summary>
    [CustomEditor(typeof(Point))]
    public class PointInspector : GeometryInspector
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            root.Bind(serializedObject);

            var displayRadiusField = new FloatField("Display Radius")
            {
                bindingPath = "DisplayRadius",
            };
            root.Add(displayRadiusField);

            root.Add(GetCommonElement());

            return root;
        }
    }
}