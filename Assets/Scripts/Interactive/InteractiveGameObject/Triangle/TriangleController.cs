using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleController : DisplayableEntityController
{
    private new TriangleModel model = null;
    public override void Init()
    {
        this.model = base.model as TriangleModel;
        model.verticesModelVec = new List<VertexModel>();

        for (int i = 0; i != model.view.transform.childCount; ++i)
        {
            VertexModel vertexModel = new VertexModel();

            vertexModel.componentsAssetID = model.componentsAssetID;
            vertexModel.codeSnippetsAssetID = model.codeSnippetsAssetID;

            vertexModel.name = "Vertex";
            vertexModel.parent = model;

            VertexView vertexView = model.view.transform.GetChild(i).gameObject.GetComponent_AutoAdd<VertexView>();
            VertexController vertexController = new VertexController();
            GameResourceManager.Instance.CombineMVC(vertexModel, vertexView, vertexController);

            vertexModel.OnPositionUpdated += InitTriangle;
            vertexModel.OnColorUpdated += (Color => InitTriangle(Vector3.zero));

            model.verticesModelVec.Add(vertexModel);
        }

        InitTriangle(Vector3.zero);
    }

    private void InitTriangle(Vector3 pos) // the parameters is useless, only used to match the onLocalPositionUpdate event
    {
        Vector3[] vertices = new Vector3[3] { model.verticesModelVec[0].position, model.verticesModelVec[1].position, model.verticesModelVec[2].position };
        int[] indexes = new int[3] { 1, 0, 2 }; // unity culling order is different from OpenGL?
        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = indexes;
        mesh.colors = new Color[3] { model.verticesModelVec[0].color, model.verticesModelVec[1].color, model.verticesModelVec[2].color };
        model.meshFilter.mesh = mesh;
    }
}
