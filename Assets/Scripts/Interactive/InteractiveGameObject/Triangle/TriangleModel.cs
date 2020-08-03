using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleModel : InteractiveGameObjectModel
{
    public override System.Type GetViewType() { return typeof(TriangleView); }
    public override System.Type GetControllerType() { return typeof(TriangleController); }

    // public Transform[] triangleVertices = new Transform[3];
    private List<InteractiveGameObjectModel> verticesModelVec = null;
    private MeshFilter meshFilter = null;

    public override void Init()
    {
        verticesModelVec = new List<InteractiveGameObjectModel>();

        for (int i = 0; i != view.transform.childCount; ++i)
        {

            InteractiveGameObjectModel vertexModel = new InteractiveGameObjectModel();
            vertexModel.name = "Vertex";
            vertexModel.parent = this;

            InteractiveGameObjectView vertexView = view.transform.GetChild(i).gameObject.AddComponent<InteractiveGameObjectView>();
            InteractiveIndicatorController vertexController = new InteractiveIndicatorController();
            GameResourceManager.Instance.CombineMVC(vertexModel, vertexView, vertexController);

            vertexModel.OnPositionUpdated += InitTriangle;
            verticesModelVec.Add(vertexModel);
        }

        meshFilter = view.GetComponent<MeshFilter>();
        InitTriangle(Vector3.zero);
        KeyboardInputManager.Instance.RegisterKeyDownMessageHandle((key) => InitTriangle(Vector3.zero), KeyCode.Z);
    }

    private void InitTriangle(Vector3 pos) // the parameters is useless, only used to match the onLocalPositionUpdate event
    {
        Vector3[] vertices = new Vector3[3] { verticesModelVec[0].position, verticesModelVec[1].position, verticesModelVec[2].position };
        Debug.Log("curr pos is " + verticesModelVec[1].position);
        int[] indexes = new int[3] { 0, 1, 2 };
        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = indexes;
        meshFilter.mesh = mesh;
    }
}
