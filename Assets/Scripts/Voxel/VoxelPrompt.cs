//------------------------------------------------------------
// 描述：体素提示器
// 作者：Z.P.Y
// 时间：2024/11/06 06:05
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Voxel
{
    /// <summary>
    /// 体素提示器
    /// </summary>
    public class VoxelPrompt : MonoBehaviour
    {
        public Vector3Int Size = Vector3Int.one;

        public EVoxelPivot Pivot = EVoxelPivot.CenterBottom;

        public Color VoxelColor = Color.green;

        public Color PivotColor = new Color(1, 0, 0, 0.5f);

        public float RotationAngleY;

        private const int MAX_SIZE_PER_AXIS = 5;

        private readonly Color m_White = Color.white;

        private Vector3Int m_Min;

        private Vector3Int m_Max;

        private Vector3Int m_Pos;

        private Vector3 m_DrawPos;

        private readonly Vector3 m_DrawOffset = Vector3.one * 0.5f;

        private readonly Vector3 m_DrawScale = Vector3.one;

        private Matrix4x4 m_TranslateTo;

        private Matrix4x4 m_TranslateBack;

        private Matrix4x4 m_RotationTo;

        private Matrix4x4 m_OldMatrix;

        private Matrix4x4 m_NewMatrix;

        private readonly Vector3 m_RotationAxis = Vector3.up;
        private readonly Vector3 m_RotationPoint = new Vector3(0.5f, 0f, 0.5f);

        private void OnDrawGizmos()
        {
            Gizmos.color = VoxelColor;
            var rotation = Quaternion.AngleAxis(RotationAngleY, m_RotationAxis);
            m_TranslateTo = Matrix4x4.Translate(m_RotationPoint);
            m_RotationTo = Matrix4x4.Rotate(rotation);
            m_TranslateBack = Matrix4x4.Translate(-m_RotationPoint);
            m_NewMatrix = m_TranslateTo * m_RotationTo * m_TranslateBack;
            m_OldMatrix = Gizmos.matrix;
            Gizmos.matrix = m_NewMatrix;
            m_Pos = transform.position.ToVector3Int();
            for (var x = m_Min.x; x <= m_Max.x; ++x)
            {
                for (var y = m_Min.y; y <= m_Max.y; ++y)
                {
                    for (var z = m_Min.z; z <= m_Max.z; ++z)
                    {
                        m_DrawPos.x = m_Pos.x + x;
                        m_DrawPos.y = m_Pos.y + y;
                        m_DrawPos.z = m_Pos.z + z;
                        m_DrawPos += m_DrawOffset;
                        Gizmos.DrawWireCube(m_DrawPos, m_DrawScale);
                        if (x == 0 && y == 0 && z == 0)
                        {
                            Gizmos.color = PivotColor;
                            Gizmos.DrawCube(m_DrawPos, m_DrawScale);
                            Gizmos.color = VoxelColor;
                        }
                    }
                }
            }
            Gizmos.color = m_White;
            Gizmos.matrix = m_OldMatrix;
        }

        private void Reset()
        {
            UpdateVoxels();
        }

        private void OnValidate()
        {
            if (Size.x < 0)
            {
                Size.x = 0;
            }

            if (Size.x > MAX_SIZE_PER_AXIS)
            {
                Size.x = MAX_SIZE_PER_AXIS;
            }

            if (Size.y < 0)
            {
                Size.y = 0;
            }

            if (Size.y > MAX_SIZE_PER_AXIS)
            {
                Size.y = MAX_SIZE_PER_AXIS;
            }

            if (Size.z < 0)
            {
                Size.z = 0;
            }

            if (Size.z > MAX_SIZE_PER_AXIS)
            {
                Size.z = MAX_SIZE_PER_AXIS;
            }

            UpdateVoxels();
        }

        private void UpdateVoxels()
        {
            switch (Pivot)
            {
                case EVoxelPivot.CenterBottom:
                    var x = Mathf.FloorToInt(Size.x * 0.5f);
                    var z = Mathf.FloorToInt(Size.z * 0.5f);
                    m_Min = new Vector3Int(-x, 0, -z);
                    break;
                case EVoxelPivot.VertexLdb:
                    m_Min = Vector3Int.zero;
                    break;
                case EVoxelPivot.VertexRdb:
                    m_Min = new Vector3Int(-Size.x + 1, 0, 0);
                    break;
                case EVoxelPivot.VertexRdf:
                    m_Min = new Vector3Int(-Size.x + 1, 0, -Size.z + 1);
                    break;
                case EVoxelPivot.VertexLdf:
                    m_Min = new Vector3Int(0, 0, -Size.z + 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            m_Max = m_Min + Size - Vector3Int.one;
        }


    }
}