using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraUIView : EntityView
{
    private new CameraUIModel model = null;

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);

        this.model = base.model as CameraUIModel;
        this.model.inputFields = GetComponentsInChildren<InputField>();
    }
}
