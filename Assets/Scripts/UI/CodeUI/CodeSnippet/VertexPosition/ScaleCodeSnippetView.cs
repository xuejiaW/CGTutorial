using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCodeSnippetView : TransformCodeSnippetView
{
    public override void Switch(bool on)
    {
        base.Switch(on);

        if (on)
            viewUpdater.UpdateView(targetGO.localScale);
        else
            viewUpdater.UpdateView(model.targetGameObject.localScale);
    }


    public override UpdateViewBase GetViewUpdater()
    {
        return new LocalScaleViewUpdater();
    }
}
