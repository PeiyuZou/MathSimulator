//------------------------------------------------------------
// 描述：Chunk计算编辑面板
// 作者：Z.P.Y
// 时间：2024/11/12 10:06
//------------------------------------------------------------

using UiElementsExtend;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Utils;

namespace PosCalculator.Editor
{
    /// <summary>
    /// Chunk计算编辑面板
    /// </summary>
    public class ChunkCalculateEditorWindow : EditorWindow
    {
        private Vector3Field m_ChunkCompressField;
        private TextField m_ChunkCompressResult;

        private LongField m_ChunkDecompressField;
        private TextField m_ChunkDecompressResult;

        private Vector3Field m_PosToOffsetField;
        private TextField m_PosToOffsetResult;

        private Vector3Field m_OffsetToPosField;
        private TextField m_OffsetToPosResult;

        private void CreateGUI()
        {
            var root = rootVisualElement;

            // Spacer
            root.Add(new LineSpacer(2));

            // Pid Compress
            CreateGUIChunkCompress();

            // Spacer
            root.Add(new LineSpacer(2));

            // Pid Decompress
            CreateGUIChunkDecompress();

            // Spacer
            root.Add(new LineSpacer(2));

            // Pos To Offset
            CreateGUIPosToOffset();

            // Spacer
            root.Add(new LineSpacer(2));

            // Offset To Pos
            CreateGUIOffsetToPos();
        }

        #region Chunk Compress

        private void CreateGUIChunkCompress()
        {
            var box = new BorderedBox("Pos Compress into Chunk Id", 8);
            rootVisualElement.Add(box);
            m_ChunkCompressField = new Vector3Field()
            {
                label = "Pos",
            };
            box.Add(m_ChunkCompressField);
            m_ChunkCompressResult = new TextField()
            {
                isReadOnly = true,
            };
            box.Add(m_ChunkCompressResult);
            var chunkCompressBtn = new Button(OnChunkCompressBtnClicked)
            {
                text = "Compress",
            };
            box.Add(chunkCompressBtn);
        }

        private void OnChunkCompressBtnClicked()
        {
            var chunkId = m_ChunkCompressField.value.PosToChunkId();
            m_ChunkCompressResult.value = chunkId.ToString();
        }

        #endregion

        #region Chunk Decompress

        private void CreateGUIChunkDecompress()
        {
            var box = new BorderedBox("Chunk Id Decompress to Pos", 8);
            rootVisualElement.Add(box);
            m_ChunkDecompressField = new LongField()
            {
                label = "Chunk Id",
            };
            box.Add(m_ChunkDecompressField);
            m_ChunkDecompressResult = new TextField()
            {
                isReadOnly = true,
            };
            box.Add(m_ChunkDecompressResult);
            var chunkDecompressBtn = new Button(OnChunkDecompressBtnClicked)
            {
                text = "Decompress",
            };
            box.Add(chunkDecompressBtn);
        }

        private void OnChunkDecompressBtnClicked()
        {
            var pos = m_ChunkDecompressField.value.ChunkIdToPos();
            m_ChunkDecompressResult.value = pos.ToString();
        }

        #endregion

        #region Pos To Offset

        private void CreateGUIPosToOffset()
        {
            var box = new BorderedBox("Pos To Offset", 8);
            rootVisualElement.Add(box);
            m_PosToOffsetField = new Vector3Field()
            {
                label = "Pos",
            };
            box.Add(m_PosToOffsetField);
            m_PosToOffsetResult = new TextField()
            {
                isReadOnly = true,
            };
            box.Add(m_PosToOffsetResult);
            var btn = new Button(OnPosToOffsetBtnClicked)
            {
                text = "Calculate",
            };
            box.Add(btn);
        }

        private void OnPosToOffsetBtnClicked()
        {
            var chunkOffset = m_PosToOffsetField.value.PosToChunkOffset();
            m_PosToOffsetResult.value = chunkOffset.ToString();
        }

        #endregion

        #region Offset To Pos

        private void CreateGUIOffsetToPos()
        {
            var box = new BorderedBox("Offset To Pos", 8);
            rootVisualElement.Add(box);
            m_OffsetToPosField = new Vector3Field()
            {
                label = "Chunk Offset",
            };
            box.Add(m_OffsetToPosField);
            m_OffsetToPosResult = new TextField()
            {
                isReadOnly = true,
            };
            box.Add(m_OffsetToPosResult);
            var btn = new Button(OnOffsetToPosBtnClicked)
            {
                text = "Calculate",
            };
            box.Add(btn);
        }

        private void OnOffsetToPosBtnClicked()
        {
            var pos = m_OffsetToPosField.value.ChunkOffsetToPos();
            m_OffsetToPosResult.value = pos.ToString();
        }

        #endregion
    }
}