using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformUIView : EntityView
{
    private new TransformUIModel model = null;

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);

        this.model = base.model as TransformUIModel;
        this.model.inputFields = GetComponentsInChildren<InputField>();
    }
}
