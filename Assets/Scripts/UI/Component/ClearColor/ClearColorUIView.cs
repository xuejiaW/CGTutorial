using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearColorUIView : ComponentView
{
    private ClearColorModel targetModel = null;
    public override void InitComponent()
    {
        base.InitComponent();
        targetModel = model.targetGameObject as ClearColorModel;
    }
    public override UpdateViewBase GetViewUpdater()
    {
        return new ColorViewUpdater();
    }

    protected override void onModelActiveUpdated(bool active)
    {
        base.onModelActiveUpdated(active);
        if (active)
            viewUpdater.UpdateView(targetModel.color);
    }
}
