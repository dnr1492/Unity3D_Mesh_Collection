using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatRectangleMesh : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    private void Start()
    {
        FlatRectangle();
    }

    /// <summary>
    /// ��� �簢�� �޽� �׸��� (����̹Ƿ� Vector2 ����ص� ����)
    /// </summary>
    private void FlatRectangle()
    {
        //���ؽ� (������ ��ǥ)
        vertices = new Vector3[] { new Vector3(-1f, 1f, 0f) /*0 (���� ��) */, new Vector3(1f, 1f, 0f) /*1 (������ ��) */,
                                             new Vector3(1f, -1f, 0f) /*2 (������ �Ʒ�) */, new Vector3(-1f, -1f, 0f) /*3 (���� �Ʒ�) */ };
        //0, 0, 0 : �߾�
        //0, 1, 0 : �߾� ��
        //0, -1, 0 : �߾� �Ʒ�

        //�ε���
        triangles = new int[] { 0, 1, 2,
                                      0, 2, 3 };

        mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
