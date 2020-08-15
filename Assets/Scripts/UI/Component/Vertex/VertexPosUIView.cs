using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexPosUIView : EntityView
{
    private new VertexPosUIModel model = null;

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);

        this.model = base.model as VertexPosUIModel;
        this.model.inputFields = GetComponentsInChildren<InputField>();
    }
}
