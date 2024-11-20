//------------------------------------------------------------
// 描述：几何相关的编辑器菜单
// 作者：Z.P.Y
// 时间：2024/11/20 10:43
//------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Geometry.Editor
{
    /// <summary>
    /// 几何相关的编辑器菜单
    /// </summary>
    public class GeometryEditorMenu : UnityEditor.Editor
    {

        [MenuItem("GameObject/MathSimulator/Geometry/Point", priority = 1)]
        public static void CreatePoint()
        {
            var gameObject = new GameObject("Point");
            gameObject.AddComponent<Point>();
            Selection.activeGameObject = gameObject;
        }

        [MenuItem("GameObject/MathSimulator/Geometry/Rectangle", priority = 2)]
        public static void CreateRectangle()
        {
            var gameObject = new GameObject("Rectangle");
            gameObject.AddComponent<Rectangle>();
            Selection.activeGameObject = gameObject;
        }

        [MenuItem("GameObject/MathSimulator/Geometry/Sector", priority = 4)]
        public static void CreateSector()
        {
            var gameObject = new GameObject("Sector");
            gameObject.AddComponent<Sector>();
            Selection.activeGameObject = gameObject;
        }
    }
}