using UnityEngine;

namespace KoryoSoft.Common.Object
{
    public class DrawMesh
    {
        /// <summary>
        /// 피벗의 위치를 설정
        /// </summary>
        public enum SetMeshBenchmark { None, Underside, Topside, Centralside }
        public SetMeshBenchmark setMeshBenchmark;

        private Mesh mesh;
        private Vector3[] vertices;
        private int[] triangles;
        private Vector3 pivot;  //피벗 위치

        /// <summary>
        /// 절차적 메쉬를 그리기 위한 데이터 설정
        /// - 폴리곤(면) 개수를 기준으로 그려냄 -
        /// - 이하 polygon의 값을 3이라 가정 -
        /// </summary>
        /// <param name="component"></param>
        /// <param name="setMeshBenchmark"></param>
        /// <param name="polygon"></param>
        /// <param name="height"></param>
        /// <param name="size"></param>
        public void SetMeshData(Component component, SetMeshBenchmark setMeshBenchmark, int polygon, float height, float size)
        {
            if (polygon < 3)
            {
                polygon = 3;
            }

            mesh = component.GetComponent<MeshFilter>().mesh;
            vertices = new Vector3[(polygon + 1) + (polygon + 1) + (polygon * 4)];  //vertices 4(밑면)+4(윗면)+12(옆면) : 20
            triangles = new int[(3 * polygon) + (3 * polygon) + (6 * polygon)];  //triangles 9(밑면)+9(윗면)+18(옆면) : 36

            #region 밑면 기준
            if (SetMeshBenchmark.Underside == setMeshBenchmark)
            {
                pivot = new Vector3(0, 0.5f, 0) * height;
                SetUnderSideMeshData(polygon, height, size, pivot);
                SetCentralMeshData(polygon, height, size, pivot);
                SetTopSideMeshData(polygon, height, size, pivot);
            }
            #endregion

            #region 윗면 기준
            else if (SetMeshBenchmark.Topside == setMeshBenchmark)
            {
                pivot = new Vector3(0, -0.5f, 0) * height;
                SetTopSideMeshData(polygon, height, size, pivot);
                SetCentralMeshData(polygon, height, size, pivot);
                SetUnderSideMeshData(polygon, height, size, pivot);
            }
            #endregion

            #region 옆면 기준
            else if (SetMeshBenchmark.Centralside == setMeshBenchmark)
            {
                pivot = new Vector3(0, 0, 0) * height;
                SetUnderSideMeshData(polygon, height, size, pivot);
                SetTopSideMeshData(polygon, height, size, pivot);
                SetCentralMeshData(polygon, height, size, pivot);
            }
            #endregion

            CreateProceduralMesh();
        }

        /// <summary>
        /// 절차적 메쉬의 밑면을 그리기 위한 데이터 설정
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="height"></param>
        /// <param name="size"></param>
        /// <param name="pivot"></param>
        private void SetUnderSideMeshData(int polygon, float height, float size, Vector3 pivot)
        {
            vertices[0] = new Vector3(0, -height / 2.0f, 0) + pivot;  //vertext 인덱스 : 0

            for (int i = 1; i <= polygon; i++)  //1,2,3
            {
                float angle = -i * (Mathf.PI * 2.0f) / polygon;  //angle : -1 * (3.14 * 2.0) / 3 == -2.09
                vertices[i] = new Vector3(Mathf.Cos(angle) * size, -height / 2.0f, Mathf.Sin(angle) * size) + pivot;  //vertext 인덱스 : 1,2,3

                ////기본 공식
                ////size와 offset 미사용
                //float angle = -i * (Mathf.PI * 2.0f) / polygon;
                //vertices[i] = new Vector3(Mathf.Cos(angle), -height / 2.0f, Mathf.Sin(angle));
            }

            for (int i = 0; i < polygon - 1; i++)  //0,1
            {
                triangles[i * 3] = 0;  //triangles 0,3 == vertices 0,0
                triangles[i * 3 + 1] = i + 2;  //triangles 1,4 == vertices 2,3
                triangles[i * 3 + 2] = i + 1;  //triangles 2,5 == vertices 1,2
            }

            triangles[3 * polygon - 3] = 0;  //triangles 6 == vertices 0
            triangles[3 * polygon - 2] = 1;  //triangles 7 == vertices 1
            triangles[3 * polygon - 1] = polygon;  //triangles 8 == vertices 3
        }

        /// <summary>
        /// 절차적 메쉬의 윗면을 그리기 위한 데이터 설정
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="height"></param>
        /// <param name="size"></param>
        /// <param name="pivot"></param>
        private void SetTopSideMeshData(int polygon, float height, float size, Vector3 pivot)
        {
            int vIdx = polygon + 1;  //vIdx(vertext 인덱스) : 4
            vertices[vIdx++] = new Vector3(0, height / 2.0f, 0) + pivot;  //vIdx++(vertext 인덱스) : 4

            for (int i = 1; i <= polygon; i++)  //1,2,3
            {
                float angle = -i * (Mathf.PI * 2.0f) / polygon;
                vertices[vIdx++] = new Vector3(Mathf.Cos(angle) * size, height / 2.0f, Mathf.Sin(angle) * size) + pivot;  //vIdx++(vertext 인덱스) : 5,6,7
            }

            int tIdx = 3 * polygon;  //tIdx(triangle 인덱스) : 9
            for (int i = 0; i < polygon - 1; i++)  //0,1
            {
                triangles[tIdx++] = polygon + 1;  //triangles 9,12 == vertices 4,5
                triangles[tIdx++] = i + 1 + polygon + 1;  //triangles 10,13 == vertices 5,6
                triangles[tIdx++] = i + 2 + polygon + 1;  //triangles 11,14 == vertices 7,8
            }

            triangles[tIdx++] = polygon + 1;  //triangles 15 == vertices 4
            triangles[tIdx++] = polygon + polygon + 1;  //triangles 16 == vertices 7
            triangles[tIdx++] = 1 + polygon + 1;  //triangles 17 == vertices 5
        }

        /// <summary>
        /// 절차적 메쉬의 옆면을 그리기 위한 데이터 설정
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="height"></param>
        /// <param name="size"></param>
        /// <param name="pivot"></param>
        private void SetCentralMeshData(int polygon, float height, float size, Vector3 pivot)
        {
            int vIdx = 0;
            vIdx = 2 * polygon + 2;  //vIdx(vertext 인덱스) : 8
            for (int i = 1; i <= polygon; i++)  //1,2,3
            {
                float angle = -i * (Mathf.PI * 2.0f) / polygon;
                vertices[vIdx++] = new Vector3(Mathf.Cos(angle) * size, -height / 2.0f, Mathf.Sin(angle) * size) + pivot;  //vIdx++(vertext 인덱스) : 8,9,10
            }

            for (int i = 1; i <= polygon; i++)  //1,2,3
            {
                float angle = -i * (Mathf.PI * 2.0f) / polygon;
                vertices[vIdx++] = new Vector3(Mathf.Cos(angle) * size, height / 2.0f, Mathf.Sin(angle) * size) + pivot;  //vIdx++(vertext 인덱스) : 11,12,13
            }

            int tIdx = 0;
            tIdx = 6 * polygon;  //tIdx(triangle 인덱스) : 18
            for (int i = 0; i < polygon - 1; i++)  //0,1
            {
                triangles[tIdx++] = (2 * polygon + 2) + i + 0;  //triangles 18,24 == vertices 8,9
                triangles[tIdx++] = (2 * polygon + 2) + i + 1;  //triangles 19,25 == vertices 9,10
                triangles[tIdx++] = (2 * polygon + 2) + i + polygon;  //triangles 20,26 == vertices 11,12

                triangles[tIdx++] = (2 * polygon + 2) + i + 1;  //triangles 21,27 == vertices 9,10
                triangles[tIdx++] = (2 * polygon + 2) + i + 1 + polygon;  //triangles 22,28 == vertices 12,13
                triangles[tIdx++] = (2 * polygon + 2) + i + polygon;  //triangles 23,29 == vertices 11,12
            }

            triangles[tIdx++] = (2 * polygon + 2) + polygon - 1;  //triangles 30 == vertices 10
            triangles[tIdx++] = (2 * polygon + 2);  //triangles 31 == vertices 8
            triangles[tIdx++] = (2 * polygon + 2) + polygon - 1 + polygon;  //triangles 32 == vertices 13

            triangles[tIdx++] = (2 * polygon + 2);  //triangles 33 == vertices 8
            triangles[tIdx++] = (2 * polygon + 2) + polygon;  //triangles 34 == vertices 11
            triangles[tIdx++] = (2 * polygon + 2) + polygon - 1 + polygon;  //triangles 35 == vertices 13
        }

        /// <summary>
        /// 절차적 메쉬 그리기
        /// </summary>
        private void CreateProceduralMesh()
        {
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }
    }
}