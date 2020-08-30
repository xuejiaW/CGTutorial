using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraComponentView : ComponentView
{
    private CameraModel targetModel = null;
    public override void InitComponent()
    {
        base.InitComponent();
        targetModel = model.targetGameObject as CameraModel;
    }
    public override UpdateViewBase GetViewUpdater()
    {
        return new CameraViewUpdater();
    }

    protected override void onModelActiveUpdated(bool active)
    {
        base.onModelActiveUpdated(active);
        if (active)
            viewUpdater.UpdateView(targetModel.cameraProperty);
    }
}
