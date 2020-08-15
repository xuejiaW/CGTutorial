using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationQuadModel : InteractiveGameObjectModel
{
    public override System.Type GetViewType() { return typeof(TransformationQuadView); }
    public override System.Type GetControllerType() { return typeof(TransformatioQuadController); }

    [System.NonSerialized]
    public MeshFilter meshFilter = null;

    [System.NonSerialized]
    public MeshCollider meshCollider = null;
}
