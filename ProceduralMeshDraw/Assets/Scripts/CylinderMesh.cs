using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderMesh : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    private float startAngle = 0;

    private void Start()
    {
        DrawCylinderMesh(18, 8);
    }

    /// <summary>
    /// ½Ç¸°´õ ¸Þ½¬ ±×¸®±â
    /// </summary>
    /// <param name="setRotationCount"></param>
    /// <param name="setAngleCount"></param>
    private void DrawCylinderMesh(int setRotationCount, int setAngleCount)
    {
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        #region Vertices
        float angle_ = 0;

        vertices = new Vector3[setRotationCount];
        Vector3 pRotation;

        for (int i = 0; i <= setRotationCount - 1; i++)
        {
            if (i == 0 || i == 9)
            {
                startAngle = 0;

                if (i == 9)
                {
                    pRotation = Vector3.down * 2;
                    vertices[i] = pRotation;
                }
                else
                {
                    pRotation = Vector3.zero;
                    vertices[i] = pRotation;
                }
            }

            if (i == 1 || i == 10)
            {
                angle_ = 360;
                startAngle = angle_;

                if (i == 10)
                {
                    pRotation = Quaternion.Euler(new Vector3(0, startAngle, 0)) * Vector3.forward / 2;
                    pRotation += vertices[9];
                    vertices[i] = pRotation;
                }
                else
                {
                    pRotation = Quaternion.Euler(new Vector3(0, startAngle, 0)) * Vector3.forward / 2;
                    vertices[i] = pRotation;
                }
            }

            if (i >= 2 && i < 9 || i >= 11)
            {
                if (angle_ >= 360)
                {
                    angle_ = 0;
                    startAngle = angle_;
                }

                if (angle_ < 360)
                {
                    angle_ = 360 / setAngleCount;
                    startAngle += angle_;

                    if (i >= 11)
                    {
                        pRotation = Quaternion.Euler(new Vector3(0, startAngle, 0)) * Vector3.forward / 2;
                        pRotation += vertices[9];
                        vertices[i] = pRotation;
                    }
                    else
                    {
                        pRotation = Quaternion.Euler(new Vector3(0, startAngle, 0)) * Vector3.forward / 2;
                        vertices[i] = pRotation;
                    }
                }
            }
        }
        #endregion

        #region Triangles
        triangles = new int[]
        {
            //Top
            //Á¤¸é
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            0, 4, 5,
            0, 5, 6,
            0, 6, 7,
            0, 7, 8,
            0, 8, 1,

            //Bottom
            //µÞ¸é
            9, 11, 10,
            9, 12, 11,
            9, 13, 12,
            9, 14, 13,
            9, 15, 14,
            9, 16, 15,
            9, 17, 16,
            9, 10, 17,

            //TopDown Quoin
            //µÞ¸é
            1, 11, 2,
            2, 12, 3,
            3, 13, 4,
            4, 14, 5,
            5, 15, 6,
            6, 16, 7,
            7, 17, 8,
            8, 10, 1,

            //BottomUp Quoin
            //µÞ¸é
            1, 10, 11,
            2, 11, 12,
            3, 12, 13,
            4, 13, 14,
            5, 14, 15,
            6, 15, 16,
            7, 16, 17,
            8, 17, 10
        };
        #endregion

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}