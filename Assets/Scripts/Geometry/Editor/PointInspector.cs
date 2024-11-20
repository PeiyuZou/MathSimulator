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
    public class PointInspector : UnityEditor.Editor
    {
        private Toggle m_IntersectionTestToggle;

        private ObjectField m_IntersectionTargetField;

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            root.Bind(serializedObject);

            var displayRadiusField = new FloatField("Display Radius")
            {
                bindingPath = "DisplayRadius",
            };
            root.Add(displayRadiusField);

            var normalColorField = new ColorField("Normal Color")
            {
                bindingPath = "NormalColor",
            };
            root.Add(normalColorField);

            var intersectColorField = new ColorField("Intersect Color")
            {
                bindingPath = "IntersectColor",
            };
            root.Add(intersectColorField);

            m_IntersectionTestToggle = new Toggle("Intersection Test")
            {
                bindingPath = "IntersectionTest",
            };
            m_IntersectionTestToggle.RegisterValueChangedCallback(OnIntersectionTestChanged);
            root.Add(m_IntersectionTestToggle);

            m_IntersectionTargetField = new ObjectField("Intersection Target")
            {
                objectType = typeof(GameObject),
                allowSceneObjects = true,
                bindingPath = "IntersectionGo",
                style =
                {
                    display = m_IntersectionTestToggle.value ? DisplayStyle.Flex : DisplayStyle.None,
                    marginLeft = 16,
                }
            };
            root.Add(m_IntersectionTargetField);

            return root;
        }

        private void OnIntersectionTestChanged(ChangeEvent<bool> evt)
        {
            m_IntersectionTargetField.style.display = evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}