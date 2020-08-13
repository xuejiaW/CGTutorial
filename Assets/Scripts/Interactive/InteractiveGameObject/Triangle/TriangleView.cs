using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleView : InteractiveGameObjectView
{
    public new TriangleModel model;
    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as TriangleModel;
        this.model.meshFilter = GetComponent<MeshFilter>();
    }
}
