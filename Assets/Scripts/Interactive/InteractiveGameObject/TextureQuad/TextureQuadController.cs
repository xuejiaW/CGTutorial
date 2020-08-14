using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureQuadController : InteractiveGameObjectController
{
    private new TextureQuadModel model = null;

    public override void Init()
    {
        this.model = base.model as TextureQuadModel;

        model.verticesModelVec = new List<InteractiveGameObjectModel>();

        for (int i = 0; i != model.view.transform.childCount; ++i)
        {
            InteractiveGameObjectModel vertexModel = new InteractiveGameObjectModel();
            vertexModel.componentsAssetID = model.componentsAssetID;
            vertexModel.name = "Vertex";
            vertexModel.parent = model;

            InteractiveGameObjectView vertexView = model.view.transform.GetChild(i).gameObject.GetComponent_AutoAdd<InteractiveGameObjectView>();
            InteractiveGameObjectController vertexController = new InteractiveGameObjectController();
            GameResourceManager.Instance.CombineMVC(vertexModel, vertexView, vertexController);

            vertexModel.OnPositionUpdated += UpdateVertexData;
            model.verticesModelVec.Add(vertexModel);
        }

        UpdateVertexData(Vector3.zero);

    }

    private void UpdateVertexData(Vector3 pos)
    {
        Vector3[] vertices = new Vector3[]{
                model.verticesModelVec[0].position,
                model.verticesModelVec[1].position,
                model.verticesModelVec[2].position,
                model.verticesModelVec[3].position  };

        Vector2[] uvs = new Vector2[]
        {
            new Vector2(0,0),
            new Vector2(2,0),
            new Vector2(2,2),
            new Vector2(0,2),
        };

        int[] indexes = new int[6] { 0, 3, 2, 0, 2, 1 };

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = indexes;
        mesh.uv = uvs;
        model.meshFilter.mesh = mesh;

    }
}
