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
        Vector3 color = (Vector3)propertyInfo.GetValue(targetModel);
        color[channel] = val;
        propertyInfo.SetValue(targetModel, color);
    }
}
