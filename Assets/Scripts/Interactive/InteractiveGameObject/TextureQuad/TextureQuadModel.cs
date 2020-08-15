using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureQuadModel : InteractiveGameObjectModel
{
    public override System.Type GetViewType() { return typeof(TextureQuadView); }
    public override System.Type GetControllerType() { return typeof(TextureQuadController); }

    [System.NonSerialized]
    public List<VertexModel> verticesModelVec = null;

    [System.NonSerialized]
    public MeshFilter meshFilter = null;
}
