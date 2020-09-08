using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVertexPos : MonoBehaviour
{
    private void Update()
    {
        Vector3[] vertices = GetComponent<MeshFilter>().mesh.vertices;
        for (int i = 0; i != vertices.Length; ++i)
        {
            Debug.Log(transform.TransformPoint(vertices[i]).ToString("f3"));
        }
        Debug.Log("---");
    }
}
