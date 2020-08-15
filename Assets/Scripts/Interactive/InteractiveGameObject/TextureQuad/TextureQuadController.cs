using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureQuadController : InteractiveGameObjectController
{
    private new TextureQuadModel model = null;
    private Vector3[] verticesPos = null;
    private Vector2[] verticesTexcoord = null;
    private int[] indexes = null;

    public override void Init()
    {
        // DO NOT trigger base init, or it will create component for texture quad

        this.model = base.model as TextureQuadModel;

        verticesPos = new Vector3[4];
        verticesTexcoord = new Vector2[4];
        indexes = new int[6] { 0, 3, 2, 0, 2, 1 };

        model.verticesModelVec = new List<VertexModel>();

        for (int i = 0; i != model.view.transform.childCount; ++i)
        {
            // TODO: refactor
            int index = i;
            VertexModel vertexModel = new VertexModel();
            vertexModel.componentsAssetID = model.componentsAssetID; // set the vertex components id
            vertexModel.name = "Vertex";
            vertexModel.parent = model;

            VertexView vertexView = model.view.transform.GetChild(i).gameObject.GetComponent_AutoAdd<VertexView>();
            VertexController vertexController = new VertexController();
            GameResourceManager.Instance.CombineMVC(vertexModel, vertexView, vertexController);

            vertexModel.OnPositionUpdated += ((pos) => UpdateVertexPos(index, pos));
            vertexModel.OnTexcoordUpdated += ((texcoord) => UpdateVertexTexcoord(index, texcoord));
            model.verticesModelVec.Add(vertexModel);
        }

        for (int i = 0; i != model.view.transform.childCount; ++i)
        {
            verticesPos[i] = model.verticesModelVec[i].position;
            verticesTexcoord[i] = model.verticesModelVec[i].texcoord;
        }
        UpdateVertexData();
    }

    private void UpdateVertexPos(int index, Vector3 pos)
    {
        verticesPos[index] = pos;
        UpdateVertexData();
    }

    private void UpdateVertexTexcoord(int index, Vector2 texcoord)
    {
        verticesTexcoord[index] = texcoord;
        UpdateVertexData();
    }

    private void UpdateVertexData()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = verticesPos;
        mesh.triangles = indexes;
        mesh.uv = verticesTexcoord;
        model.meshFilter.mesh = mesh;
    }
}
