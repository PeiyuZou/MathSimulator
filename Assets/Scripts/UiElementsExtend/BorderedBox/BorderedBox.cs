//------------------------------------------------------------
// 描述：带边框的Box
// 作者：Z.P.Y
// 时间：2024/11/11 04:59
//------------------------------------------------------------

using UnityEditor;
using UnityEngine.UIElements;

namespace UiElementsExtend
{
    /// <summary>
    /// 带边框的Box
    /// </summary>
    public class BorderedBox : GroupBox
    {
        public BorderedBox(string title = null, float margin = 4f)
        {
            var uss = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/UiElementsExtend/BorderedBox/BorderedBox.uss");
            styleSheets.Add(uss);
            if (!string.IsNullOrEmpty(title))
            {
                var titleLabel = new Label(title);
                titleLabel.AddToClassList("title-label");
                Add(titleLabel);
            }
            style.marginLeft = margin;
            style.marginRight = margin;
        }
    }
}