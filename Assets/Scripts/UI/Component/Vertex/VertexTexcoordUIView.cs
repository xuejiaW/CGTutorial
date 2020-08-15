using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexTexcoordUIView : EntityView
{
    private new VertexTexcoordUIModel model = null;

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);

        this.model = base.model as VertexTexcoordUIModel;
        this.model.inputFields = GetComponentsInChildren<InputField>();
    }
}
