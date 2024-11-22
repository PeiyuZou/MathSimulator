//------------------------------------------------------------
// 描述：COMMENTS
// 作者：Z.P.Y
// 时间：2024/11/21 02:46
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
    [CustomEditor(typeof(Rectangle))]
    public class RectangleInspector : GeometryInspector
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            root.Bind(serializedObject);

            var sizeField = new Vector2Field("Size")
            {
                bindingPath = "Size",
            };
            root.Add(sizeField);

            var common = GetCommonElement();
            root.Add(common);

            return root;
        }
    }
}