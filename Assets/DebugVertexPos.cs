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
            Vector2 screen = MainManager.Instance.screenCamera.WorldToScreenPoint(transform.TransformPoint(vertices[i]));
            screen[0] -= (Screen.width / 2);
            Debug.Log(screen);
        }
        Debug.Log("---");
    }
}
