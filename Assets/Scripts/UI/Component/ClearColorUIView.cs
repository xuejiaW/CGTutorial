using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearColorUIView : EntityView
{
    private new ClearColorModel model = null;

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);

        this.model = base.model as ClearColorModel;
        this.model.inputFields = GetComponentsInChildren<InputField>();
    }
}
