using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformComponentController : ComponentController
{
    protected DisplayableEntityModel targetGameObject
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public override void InitComponent()
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

    public override void UpdateModelProperty(int channel, string val)
    {
        // modelUpdater.SetTargetModel(targetGameObject);
        modelUpdater.UpdateModelProperty(channel, val);
    }

    ~TransformComponentController()
    {
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged -= OnIndicatorChanged;
    }
}
