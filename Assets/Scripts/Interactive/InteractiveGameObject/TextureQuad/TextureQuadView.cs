using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureQuadView : EntityView
{
    public new TextureQuadModel model;
    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as TextureQuadModel;
        this.model.meshFilter = GetComponent<MeshFilter>();
    }
}
