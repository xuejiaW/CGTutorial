using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexPosUIView : TransformComponentView
{
    protected override void onModelActiveUpdated(bool active)
    {
        base.onModelActiveUpdated(active);
        if (active)
            viewUpdater.UpdateView(targetGameObject.localPosition);
        else
            viewUpdater.UpdateView(model.targetGameObject.localPosition);
    }

    public override UpdateViewBase GetViewUpdater()
    {
        return new LocalPositionViewUpdater();
    }
}
