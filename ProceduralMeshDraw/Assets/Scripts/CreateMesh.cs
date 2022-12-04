using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{
    public int polygon = 3;  //������(��)�� ��
    public float height = 1.0f;  //������(��)�� ����
    public float size = 1.0f;

    private void Start()
    {
        KoryoSoft.Common.Object.DrawMesh drawMesh = new KoryoSoft.Common.Object.DrawMesh();
        drawMesh.setMeshBenchmark = KoryoSoft.Common.Object.DrawMesh.SetMeshBenchmark.Topside;
        drawMesh.SetMeshData(this, drawMesh.setMeshBenchmark, polygon, height, size);
    }

    /// <summary>
    /// Inspector���� ���� ����� ���� ȣ��
    /// </summary>
    private void OnValidate()
    {
        KoryoSoft.Common.Object.DrawMesh drawMesh = new KoryoSoft.Common.Object.DrawMesh();
        drawMesh.setMeshBenchmark = KoryoSoft.Common.Object.DrawMesh.SetMeshBenchmark.Topside;
        drawMesh.SetMeshData(this, drawMesh.setMeshBenchmark, polygon, height, size);
    }
}
