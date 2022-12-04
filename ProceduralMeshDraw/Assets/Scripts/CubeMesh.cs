using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMesh : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    private void Start()
    {
        Cube_Box();
    }

    /// <summary>
    /// ť��(�ڽ�) �޽� ����� (ť���̹Ƿ� �ĸ��� �׸� �ʿ� �����Ƿ� �׸��� ����) (�� index 23)
    /// </summary>
    private void Cube_Box()
    {
        MeshFilter filter = this.GetComponent<MeshFilter>();
        mesh = filter.mesh;

        //��� ���� �����Ϳ� ��� �ﰢ�� �ε����� ����ϴ�.
        //�ﰢ�� �迭�� �ٽ� �ۼ��ϱ� ���� �� �Լ��� ȣ���ؾ� �մϴ�
        mesh.Clear();

        float length = 1f;
        float width = 1f;
        float height = 1f;

        #region Vertices
        Vector3 p0 = new Vector3(-length * .5f, -width * .5f, height * .5f);  //���� �Ʒ� ��
        Vector3 p1 = new Vector3(length * .5f, -width * .5f, height * .5f);  //������ �Ʒ� ��
        Vector3 p2 = new Vector3(length * .5f, -width * .5f, -height * .5f);  //������ �Ʒ� ��
        Vector3 p3 = new Vector3(-length * .5f, -width * .5f, -height * .5f);  //���� �Ʒ� ��
        Vector3 p4 = new Vector3(-length * .5f, width * .5f, height * .5f);  //���� �� ��
        Vector3 p5 = new Vector3(length * .5f, width * .5f, height * .5f);  //������ �� ��
        Vector3 p6 = new Vector3(length * .5f, width * .5f, -height * .5f);  //������ �� ��
        Vector3 p7 = new Vector3(-length * .5f, width * .5f, -height * .5f);  //���� �� ��

        vertices = new Vector3[]
        {
            //Bottom
            p0, p1, p2, p3,

            //Left
            p7, p4, p0, p3,

            //Front
            p4, p5, p1, p0,

            //Back
            p6, p7, p3, p2,

            //Right
            p5, p6, p2, p1,

            //Top
            p7, p6, p5, p4
        };
        #endregion

        #region Normales
        Vector3 up = Vector3.up;
        Vector3 down = Vector3.down;
        Vector3 front = Vector3.forward;
        Vector3 back = Vector3.back;
        Vector3 left = Vector3.left;
        Vector3 right = Vector3.right;

        Vector3[] normales = new Vector3[]
        {
            //Bottom
            down, down, down, down,

            //Left
            left, left, left, left,

            //Front
            front, front, front, front,

            //Back
            back, back, back, back,

            //Right
            right, right, right, right,

            //Top
            up, up, up, up
        };
        #endregion

        #region UVs
        Vector2 _00 = new Vector2(0f, 0f);  //���� �Ʒ�
        Vector2 _10 = new Vector2(1f, 0f);  //������ �Ʒ�
        Vector2 _01 = new Vector2(0f, 1f);  //���� ��
        Vector2 _11 = new Vector2(1f, 1f);  //������ ��

        Vector2[] uvs = new Vector2[]
        {
            //Bottom
            _11, _01, _00, _10,  //3

            //Left
            _11, _01, _00, _10,  //7

            //Front
            _11, _01, _00, _10,  //11

            //Back
            _11, _01, _00, _10,  //15

            //Right
            _11, _01, _00, _10,  //19

            //Top
            _11, _01, _00, _10,  //23
        };
        #endregion

        #region Triangles
        triangles = new int[]
        {
            //Bottom
            3, 1, 0,
                  3, 2, 1,

            //Left
            7, 5, 4,
                  7, 6, 5,

            //Front
            11, 9, 8,
                  11, 10, 9,

            //Back
            15, 13, 12,
                  15, 14, 13,

            //Right
            19, 17, 16,
                  19, 18, 17,

            //Top
            23, 21, 20,
                  3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,  //23, 22, 21
        };
        #endregion

        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        //�޽� �����͸� ����ȭ�Ͽ� ������ ������ ���
        //�� �� ����Ǳ� ������ ������Ʈ���� ������ ������ �߿����� ���� ��쿡�� ���
        mesh.RecalculateBounds();

        //�������� �޽��� ��� ������ �ٽ� �����
        //������ ������ �� ��� ������ �ùٸ��� Ȯ���Ϸ��� �� �Լ��� ȣ���ؾ� ��
        mesh.Optimize();
    }
}
