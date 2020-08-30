using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCodeSnippetController : CodeSnippetController
{
    protected DisplayableEntityModel targetGameObject
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public override void InitCodeSnippet()
    {
        modelUpdater.SetTargetModel(targetGameObject);
    }
}
