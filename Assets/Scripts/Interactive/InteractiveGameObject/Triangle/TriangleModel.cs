using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleModel : InteractiveGameObjectModel
{
    public override System.Type GetViewType() { return typeof(TriangleView); }
    public override System.Type GetControllerType() { return typeof(TriangleController); }

    [System.NonSerialized]
    public List<VertexModel> verticesModelVec = null;

    [System.NonSerialized]
    public MeshFilter meshFilter = null;
}
