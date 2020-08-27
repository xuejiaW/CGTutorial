using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorSnippetController : CodeSnippetController
{
    public IUpdateModelProperty clearColorUpdater = null;

    public override void InitCodeSnippet()
    {
        base.InitCodeSnippet();
        clearColorUpdater = new ClearColorModelUpdater();
    }

    public void UpdateClearColor(int channel, string val)
    {
        clearColorUpdater.UpdateModelProperty(model.targetGameObject, channel, val);
    }
}
