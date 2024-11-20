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
    public class SectorInspector : UnityEditor.Editor
    {
        private Toggle m_IntersectionTestToggle;

        private ObjectField m_IntersectionTargetField;

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

            root.Bind(serializedObject);

            return root;
        }

        private void OnIntersectionTestChanged(ChangeEvent<bool> evt)
        {
            m_IntersectionTargetField.style.display = evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}