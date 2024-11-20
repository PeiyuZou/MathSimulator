//------------------------------------------------------------
// 描述：位置计算器编辑面板
// 作者：Z.P.Y
// 时间：2024/11/11 09:42
//------------------------------------------------------------

using System;
using UiElementsExtend;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Utils;

namespace PosCalculator.Editor
{
    /// <summary>
    /// Pid计算编辑面板
    /// </summary>
    public class PidCalculateEditorWindow : EditorWindow
    {
        private EnumField m_ArithmeticField;
        private Vector3Field m_PidCompressField;
        private TextField m_PidCompressResult;
        private LongField m_PidDecompressField;
        private TextField m_PidDecompressResult;

        private void CreateGUI()
        {
            var root = rootVisualElement;
            // Arithmetic
            m_ArithmeticField = new EnumField("Arithmetic", EPidCompress.Bit20);
            root.Add(m_ArithmeticField);

            // Spacer
            root.Add(new LineSpacer(2));

            // Pid Compress
            CreateGUIPosCompress();

            // Spacer
            root.Add(new LineSpacer(2));

            // Pid Decompress
            CreateGUIPidDecompress();

            // Spacer
            root.Add(new LineSpacer(2));
        }

        #region Pos Compress

        private void CreateGUIPosCompress()
        {
            var box = new BorderedBox("Pos Compress into Pid", 8);
            rootVisualElement.Add(box);
            m_PidCompressField = new Vector3Field()
            {
                label = "Pos",
            };
            box.Add(m_PidCompressField);
            m_PidCompressResult = new TextField()
            {
                isReadOnly = true,
            };
            box.Add(m_PidCompressResult);
            var pidCompressBtn = new Button(OnPosCompressBtnClicked)
            {
                text = "Compress",
            };
            box.Add(pidCompressBtn);
        }

        private void OnPosCompressBtnClicked()
        {
            var compress = (EPidCompress)m_ArithmeticField.value;
            var pid = compress switch
            {
                EPidCompress.Bit20 => m_PidCompressField.value.Compress_20(),
                EPidCompress.Bit20X10 => m_PidCompressField.value.Compress_20x10(),
                _ => throw new ArgumentOutOfRangeException()
            };
            m_PidCompressResult.value = pid.ToString();
        }

        #endregion

        #region Pid Decompress

        private void CreateGUIPidDecompress()
        {
            var box = new BorderedBox("Pid Decompress to Pos", 8);
            rootVisualElement.Add(box);
            m_PidDecompressField = new LongField()
            {
                label = "Pid",
            };
            box.Add(m_PidDecompressField);
            m_PidDecompressResult = new TextField()
            {
                isReadOnly = true,
            };
            box.Add(m_PidDecompressResult);
            var pidDecompressBtn = new Button(OnPidDecompressBtnClicked)
            {
                text = "Decompress",
            };
            box.Add(pidDecompressBtn);
        }

        private void OnPidDecompressBtnClicked()
        {
            var compress = (EPidCompress)m_ArithmeticField.value;
            var pos = compress switch
            {
                EPidCompress.Bit20 => m_PidDecompressField.value.Decompress_20(),
                EPidCompress.Bit20X10 => m_PidDecompressField.value.Decompress_20x10(),
                _ => throw new ArgumentOutOfRangeException()
            };
            m_PidDecompressResult.value = pos.ToString();
        }

        #endregion
    }
}