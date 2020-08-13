using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleController : DisplayableEntityController
{
    private new TriangleModel model = null;
    public override void Init()
    {
        this.model = base.model as TriangleModel;
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

            vertexModel.OnPositionUpdated += InitTriangle;
            model.verticesModelVec.Add(vertexModel);
        }

        InitTriangle(Vector3.zero);
        KeyboardInputManager.Instance.RegisterKeyDownMessageHandle((key) => InitTriangle(Vector3.zero), KeyCode.Z);
    }

    private void InitTriangle(Vector3 pos) // the parameters is useless, only used to match the onLocalPositionUpdate event
    {

        Vector3[] vertices = new Vector3[3] { model.verticesModelVec[0].position, model.verticesModelVec[1].position, model.verticesModelVec[2].position };
        int[] indexes = new int[3] { 1, 0, 2 }; // unity culling order is different from OpenGL?
        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = indexes;
        model.meshFilter.sharedMesh = mesh;
        model.meshFilter.mesh = mesh;
    }
}
