using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTransformUIView : ComponentView
{
    private new CameraTransformUIModel model = null;

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);

        this.model = base.model as CameraTransformUIModel;
        this.model.inputFields = GetComponentsInChildren<InputField>();
    }
}
