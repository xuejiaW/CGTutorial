using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TexcoordComponentView : ComponentView
{
    private VertexModel targetModel = null;
    public override void InitComponent()
    {
        base.InitComponent();
        targetModel = model.targetGameObject as VertexModel;
    }
    public override UpdateViewBase GetViewUpdater()
    {
        return new TexcoordViewUpdater();
    }

    protected override void onModelActiveUpdated(bool active)
    {
        base.onModelActiveUpdated(active);
        if (active)
            viewUpdater.UpdateView(targetModel.texcoord);
    }

}
