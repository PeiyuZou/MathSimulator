//------------------------------------------------------------
// 描述：扇形Inspector定制
// 作者：Z.P.Y
// 时间：2024/11/20 11:55
//------------------------------------------------------------

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Geometry.Editor
{
    /// <summary>
    /// 扇形Inspector定制
    /// </summary>
    [CustomEditor(typeof(Sector))]
    public class SectorInspector : GeometryInspector
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            var radiusField = new FloatField("Radius")
            {
                bindingPath = "Radius",
            };
            root.Add(radiusField);

            var angleField = new FloatField("Angle")
            {
                bindingPath = "Angle",
            };
            root.Add(angleField);

            var common = GetCommonElement();
            root.Add(common);

            root.Bind(serializedObject);

            return root;
        }
    }
}