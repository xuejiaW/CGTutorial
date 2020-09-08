using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPositionModelUpdater : UpdateModelPropertyBase
{
    public override void SetTargetModel(EntityModel model)
    {
        base.SetTargetModel(model);
        propertyInfo = targetModel.GetType().GetProperty("localPosition");
    }

    public override void UpdateModelProperty(int channel, string value)
    {
        float.TryParse(value, out float val);
        Vector3 localPos = (Vector3)propertyInfo.GetValue(targetModel);
        if (channel == 2)
            val *= -1;
        localPos[channel] = val;
        propertyInfo.SetValue(targetModel, localPos);
    }
}
