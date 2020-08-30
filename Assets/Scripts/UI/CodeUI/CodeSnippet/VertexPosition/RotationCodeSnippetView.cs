using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCodeSnippetView : TransformCodeSnippetView
{
    public override void Switch(bool on)
    {
        base.Switch(on);

        if (on)
            viewUpdater.UpdateView(targetGO.localRotation);
        else
            viewUpdater.UpdateView(model.targetGameObject.localRotation);
    }


    public override UpdateViewBase GetViewUpdater()
    {
        return new LocalRotationViewUpdater();
    }
}
