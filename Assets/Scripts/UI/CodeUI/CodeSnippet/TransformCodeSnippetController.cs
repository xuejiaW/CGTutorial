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
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged += OnIndicatorChanged;
    }

    protected virtual void OnIndicatorChanged(InteractiveIndicatorController oldIndicator, InteractiveIndicatorController newIndicator)
    {
        // unregister event in old target and register event in new target
        // modelUpdater.SetTargetModel(oldIndicator.model);
        modelUpdater.SetTargetModel(newIndicator.model);
    }

    ~TransformCodeSnippetController()
    {
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged -= OnIndicatorChanged;
    }
}
