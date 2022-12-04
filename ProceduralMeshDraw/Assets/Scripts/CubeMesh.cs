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
    /// 큐브(박스) 메쉬 만들기 (큐브이므로 후면을 그릴 필요 없으므로 그리지 않음) (총 index 23)
    /// </summary>
    private void Cube_Box()
    {
        MeshFilter filter = this.GetComponent<MeshFilter>();
        mesh = filter.mesh;

        //모든 정점 데이터와 모든 삼각형 인덱스를 지웁니다.
        //삼각형 배열을 다시 작성하기 전에 이 함수를 호출해야 합니다
        mesh.Clear();

        float length = 1f;
        float width = 1f;
        float height = 1f;

        #region Vertices
        Vector3 p0 = new Vector3(-length * .5f, -width * .5f, height * .5f);  //왼쪽 아래 앞
        Vector3 p1 = new Vector3(length * .5f, -width * .5f, height * .5f);  //오른쪽 아래 앞
        Vector3 p2 = new Vector3(length * .5f, -width * .5f, -height * .5f);  //오른쪽 아래 뒤
        Vector3 p3 = new Vector3(-length * .5f, -width * .5f, -height * .5f);  //왼쪽 아래 뒤
        Vector3 p4 = new Vector3(-length * .5f, width * .5f, height * .5f);  //왼쪽 위 앞
        Vector3 p5 = new Vector3(length * .5f, width * .5f, height * .5f);  //오른쪽 위 앞
        Vector3 p6 = new Vector3(length * .5f, width * .5f, -height * .5f);  //오른쪽 위 뒤
        Vector3 p7 = new Vector3(-length * .5f, width * .5f, -height * .5f);  //왼쪽 위 뒤

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
        Vector2 _00 = new Vector2(0f, 0f);  //왼쪽 아래
        Vector2 _10 = new Vector2(1f, 0f);  //오른쪽 아래
        Vector2 _01 = new Vector2(0f, 1f);  //왼쪽 위
        Vector2 _11 = new Vector2(1f, 1f);  //오른쪽 위

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

        //메쉬 데이터를 최적화하여 렌더링 성능을 향상
        //둘 다 변경되기 때문에 지오메트리와 정점의 순서가 중요하지 않은 경우에만 사용
        mesh.RecalculateBounds();

        //정점에서 메쉬의 경계 볼륨을 다시 계산함
        //정점을 수정한 후 경계 볼륨이 올바른지 확인하려면 이 함수를 호출해야 함
        mesh.Optimize();
    }
}
