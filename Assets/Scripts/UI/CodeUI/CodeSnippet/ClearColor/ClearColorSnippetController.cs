using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorSnippetController : CodeSnippetController
{
    public ClearColorModel targetModel = null;

    public override void InitCodeSnippet()
    {
        base.InitCodeSnippet();

        targetModel = model.targetGameObject as ClearColorModel;
    }

    public void UpdateClearColor(int channel, string val)
    {
        float.TryParse(val, out float value);
        Color color = targetModel.clearColor;
        color[channel] = value;
        targetModel.clearColor = color;
    }
}
