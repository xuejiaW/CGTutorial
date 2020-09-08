using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCube : MonoBehaviour
{
    private MeshFilter filter = null;

    Vector2[] uv =
    {
        new Vector3( 0, 0),
        new Vector3( 1, 0),
        new Vector3( 1, 1),
        new Vector3( 0, 1),

        // back face
        new Vector2(0, 0),
        new Vector2(1, 0),
        new Vector2(1, 1),
        new Vector2(0, 1),

        // right face
        new Vector2(0, 0),
        new Vector2(1, 0),
        new Vector2(1, 1),
        new Vector2(0, 1),

        // left face
        new Vector2(0, 0),
        new Vector2(1, 0),
        new Vector2(1, 1),
        new Vector2(0, 1),

        // top face
        new Vector2(0, 0),
        new Vector2(1, 0),
        new Vector2(1, 1),
        new Vector2(0, 1),

        // bottom face
        new Vector2(0, 0),
        new Vector2(1, 0),
        new Vector2(1, 1),
        new Vector2(0, 1)
    };

    Vector3[] vertices = {
        // front face
        new Vector3(-0.5f, -0.5f, -0.5f),
        new Vector3(+0.5f, -0.5f, -0.5f),
        new Vector3(+0.5f, +0.5f, -0.5f),
        new Vector3(-0.5f, +0.5f, -0.5f),

        // back face
        new Vector3(+0.5f, -0.5f, +0.5f),
        new Vector3(-0.5f, -0.5f, +0.5f),
        new Vector3(-0.5f, +0.5f, +0.5f),
        new Vector3(+0.5f, +0.5f, +0.5f),

        // right face
        new Vector3(+0.5f, -0.5f, -0.5f),
        new Vector3(+0.5f, -0.5f, +0.5f),
        new Vector3(+0.5f, +0.5f, +0.5f),
        new Vector3(+0.5f, +0.5f, -0.5f),

        // left face
        new Vector3(-0.5f, -0.5f, +0.5f),
        new Vector3(-0.5f, -0.5f, -0.5f),
        new Vector3(-0.5f, +0.5f, -0.5f),
        new Vector3(-0.5f, +0.5f, +0.5f),

        // top face
        new Vector3(-0.5f, +0.5f, -0.5f),
        new Vector3(+0.5f, +0.5f, -0.5f),
        new Vector3(+0.5f, +0.5f, +0.5f),
        new Vector3(-0.5f, +0.5f, +0.5f),

        // bottom face
        new Vector3(-0.5f, -0.5f, +0.5f),
        new Vector3(+0.5f, -0.5f, +0.5f),
        new Vector3(+0.5f, -0.5f, -0.5f),
        new Vector3(-0.5f, -0.5f, -0.5f)};

    int[] index =
        {
            1, 0, 2,
            2, 0, 3,
            5, 4, 6,
            6, 4, 7,
            9, 8, 10,
            10, 8, 11,
            13, 12, 14,
            14, 12, 15,
            17, 16, 18,
            18, 16, 19,
            21, 20, 22,
            22, 20, 23};

    private void Start()
    {
        filter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = index;
        mesh.uv = uv;
        filter.mesh = mesh;
    }
}
