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
    }

    public override void UpdateModelProperty(int channel, string val)
    {
        // modelUpdater.SetTargetModel(targetGameObject);
        modelUpdater.UpdateModelProperty(channel, val);
    }
}
