using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformatioQuadController : InteractiveGameObjectController
{
    private new TransformationQuadModel model = null;

    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as TransformationQuadModel;
    }

    public override void Init()
    {
        base.Init();

        Vector3[] verticesPos = new Vector3[]
        {
           new Vector3(-0.5f,-0.5f,0),
           new Vector3(0.5f,-0.5f,0),
           new Vector3(0.5f,0.5f,0),
           new Vector3(-0.5f,0.5f,0),
        };

        Vector2[] uvs = new Vector2[]
        {
            new Vector2(0,0),
            new Vector2(1,0),
            new Vector2(1,1),
            new Vector2(0,1),
        };

        int[] indexes = new int[6] { 0, 3, 2, 0, 2, 1 };

        Mesh mesh = new Mesh();
        mesh.vertices = verticesPos;
        mesh.triangles = indexes;
        mesh.uv = uvs;
        model.meshFilter.mesh = mesh;
        model.meshCollider.sharedMesh = mesh;
    }
}
