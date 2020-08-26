using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorUIController : ComponentController
{
    private new ClearColorUIModel model = null;
    private ClearColorModel targetModel = null;

    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as ClearColorUIModel;
    }

    public override void InitComponent()
    {
        base.InitComponent();
        targetModel = model.targetGameObject as ClearColorModel;
    }

    public void UpdateCameraClearColor(int channel, string value)
    {
        float.TryParse(value, out float val);

        Color backgroundColor = targetModel.clearColor;
        backgroundColor[channel] = val;
        targetModel.clearColor = backgroundColor;
    }
}
