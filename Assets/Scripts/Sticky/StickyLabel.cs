//------------------------------------------------------------
// 描述：便签文本框
// 作者：Z.P.Y
// 时间：2024/11/05 04:27
//------------------------------------------------------------

using System;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Sticky
{
    /// <summary>
    /// 便签文本框
    /// </summary>
    public class StickyLabel : MonoBehaviour
    {
        public string Label;

        public Vector3 Offset;

        private Vector3 m_DrawPos;

        private void OnDrawGizmos()
        {
            m_DrawPos = transform.position + Offset;
            Handles.Label(m_DrawPos, Label);
        }
    }
}