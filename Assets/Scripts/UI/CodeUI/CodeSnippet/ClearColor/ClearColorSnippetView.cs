using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorSnippetView : CodeSnippetView
{
    public ClearColorModel targetModel = null;

    public IUpdateComponent<Color> componentUpdater = null;

    public override void InitCodeSnippet()
    {
        base.InitCodeSnippet();

        targetModel = model.targetGameObject as ClearColorModel;

        componentUpdater = new ColorComponentUpdater();
        componentUpdater.SetTargetInputFields(snippetInputsList);
        targetModel.OnClearColorChanged += (color => componentUpdater.UpdateComponent(color));
    }
}
