using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationQuadView : InteractiveGameObjectView
{
    public new TransformationQuadModel model;
    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as TransformationQuadModel;
        this.model.meshFilter = GetComponent<MeshFilter>();
        this.model.meshCollider = GetComponent<MeshCollider>();
    }
}
