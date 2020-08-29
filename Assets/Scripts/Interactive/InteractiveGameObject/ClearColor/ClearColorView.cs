using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorView : InteractiveGameObjectView
{
    public new ClearColorModel model = null;

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as ClearColorModel;
        this.model.OnColorChanged += UpdateCameraClearColor;
    }

    private void UpdateCameraClearColor(Color color)
    {
        MainManager.Instance.worldCamera.backgroundColor = color;
        MainManager.Instance.screenCamera.backgroundColor = color;
    }
}
