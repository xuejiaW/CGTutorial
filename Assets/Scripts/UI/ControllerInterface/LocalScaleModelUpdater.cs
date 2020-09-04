using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalScaleModelUpdater : UpdateModelPropertyBase
{
    public override void SetTargetModel(EntityModel model)
    {
        base.SetTargetModel(model);
        propertyInfo = targetModel.GetType().GetProperty("localScale");
    }

    public override void UpdateModelProperty(int channel, string value)
    {
        float.TryParse(value, out float val);
        bool holdingGO = InteractiveGameObjectCollection.Instance.holdingInteractiveGo != null;
        Vector3 currLocalScale = (targetModel as DisplayableEntityModel).localScale;
        Vector3 holdingGOScale = InteractiveGameObjectCollection.Instance.holdingInteractiveGo != null
                                 ? InteractiveGameObjectCollection.Instance.holdingInteractiveGo.localScale : Vector3.one;

        currLocalScale[channel] = val / holdingGOScale[channel];

        propertyInfo.SetValue(targetModel, currLocalScale);

    }
}
