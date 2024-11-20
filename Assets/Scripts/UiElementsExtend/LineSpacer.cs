//------------------------------------------------------------
// 描述：空白行
// 作者：Z.P.Y
// 时间：2024/11/11 04:37
//------------------------------------------------------------

using UnityEngine.UIElements;

namespace UiElementsExtend
{
    /// <summary>
    /// 空白行
    /// </summary>
    public class LineSpacer : VisualElement
    {
        public LineSpacer(float height)
        {
            style.height = height;
        }
    }
}