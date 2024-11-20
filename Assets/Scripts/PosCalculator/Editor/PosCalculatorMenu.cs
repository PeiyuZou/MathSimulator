//------------------------------------------------------------
// 描述：位置计算编辑器菜单
// 作者：Z.P.Y
// 时间：2024/11/11 10:10
//------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace PosCalculator.Editor
{
    /// <summary>
    /// 位置计算编辑器菜单
    /// </summary>
    public class PosCalculatorMenu : UnityEditor.Editor
    {
        [MenuItem("Menu/Pos Calculator/Pid Calculator %q")]
        public static void OpenPidCalculatorWindow()
        {
            var window = EditorWindow.GetWindow(typeof(PidCalculateEditorWindow), false, "Pos Calculator", true);
            window.minSize = new Vector2(600, 240);
            window.Show();
        }

        [MenuItem("Menu/Pos Calculator/Chunk Calculator %w")]
        public static void OpenChunkCalculatorWindow()
        {
            var window = EditorWindow.GetWindow(typeof(ChunkCalculateEditorWindow), false, "Chunk Calculator", true);
            window.minSize = new Vector2(600, 430);
            window.Show();
        }
    }
}