using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleComponentView : TransformComponentView
{
    protected override void onModelActiveUpdated(bool active)
    {
        base.onModelActiveUpdated(active);
        if (active)
            viewUpdater.UpdateView(targetGameObject.localScale);
        else
            viewUpdater.UpdateView(model.targetGameObject.localScale);
    }


    public override UpdateViewBase GetViewUpdater()
    {
        return new LocalScaleViewUpdater();
    }
}
