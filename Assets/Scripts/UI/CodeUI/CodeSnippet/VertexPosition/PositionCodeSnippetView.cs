using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCodeSnippetView : TransformCodeSnippetView
{
    public override void Switch(bool on)
    {
        base.Switch(on);

        if (on)
            viewUpdater.UpdateView(targetGO.localPosition);
        else
            viewUpdater.UpdateView(model.targetGameObject.localPosition);
    }


    public override UpdateViewBase GetViewUpdater()
    {
        return new LocalPositionViewUpdater();
    }
}
