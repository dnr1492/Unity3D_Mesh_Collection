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
    /// 평면 사각형 메쉬 그리기 (평면이므로 Vector2 사용해도 무관)
    /// </summary>
    private void FlatRectangle()
    {
        //버텍스 (꼭지점 좌표)
        vertices = new Vector3[] { new Vector3(-1f, 1f, 0f) /*0 (왼쪽 위) */, new Vector3(1f, 1f, 0f) /*1 (오른쪽 위) */,
                                             new Vector3(1f, -1f, 0f) /*2 (오른쪽 아래) */, new Vector3(-1f, -1f, 0f) /*3 (왼쪽 아래) */ };
        //0, 0, 0 : 중앙
        //0, 1, 0 : 중앙 위
        //0, -1, 0 : 중앙 아래

        //인덱스
        triangles = new int[] { 0, 1, 2,
                                      0, 2, 3 };

        mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
