using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrientationComponentView : TransformComponentView
{
    protected override void onModelActiveUpdated(bool active)
    {
        base.onModelActiveUpdated(active);
        if (active)
            viewUpdater.UpdateView(targetGameObject.localRotation);
        else
            viewUpdater.UpdateView(model.targetGameObject.localRotation);
    }

    public override UpdateViewBase GetViewUpdater()
    {
        return new OrientationViewUpdater();
    }
}
