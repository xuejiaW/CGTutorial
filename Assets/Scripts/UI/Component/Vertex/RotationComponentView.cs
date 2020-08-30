using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationComponentView : TransformComponentView
{
    protected override void onModelActiveUpdated(bool active)
    {
        base.onModelActiveUpdated(active);
        if (active)
            viewUpdater.UpdateView(targetGameObject.localRotation);
        else
            viewUpdater.UpdateView(model.targetGameObject.localRotation);
    }

    protected override void OnIndicatorChanged(InteractiveIndicatorController oldIndicator, InteractiveIndicatorController newIndicator)
    {
        base.OnIndicatorChanged(oldIndicator, newIndicator);
        Debug.Log(" On Indicator changed ");
        //TODO: bug 数据没有更新
        // viewUpdater.UpdateView(targetGameObject.localRotation);
        viewUpdater.UpdateView(model.targetGameObject.localRotation);
    }

    public override UpdateViewBase GetViewUpdater()
    {
        return new LocalRotationViewUpdater();
    }
}
