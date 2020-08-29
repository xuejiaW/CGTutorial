using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexPosUIController : ComponentController
{
    private DisplayableEntityModel targetGameObject
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public override IUpdateModelProperty GetModelUpdater()
    {
        return new LocalPositionModelUpdater();
    }

    public override void UpdateModelProperty(int channel, string val)
    {
        modelUpdater.UpdateModelProperty(targetGameObject, channel, val);
    }
}
