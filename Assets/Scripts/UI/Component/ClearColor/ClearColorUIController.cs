using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorUIController : ComponentController
{
    private IUpdateModelProperty clearColorUpdater = null;

    public override void InitComponent()
    {
        base.InitComponent();
        clearColorUpdater = new ClearColorModelUpdater();
    }

    public void UpdateClearColor(int channel, string value)
    {
        clearColorUpdater.UpdateModelProperty(model.targetGameObject, channel, value);
    }
}
