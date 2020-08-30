using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexcoordModelUpdater : UpdateModelPropertyBase
{
    public override void SetTargetModel(EntityModel model)
    {
        base.SetTargetModel(model);
        propertyInfo = targetModel.GetType().GetProperty("texcoord");
    }

    public override void UpdateModelProperty(int channel, string value)
    {
        float.TryParse(value, out float val);
        Vector2 texcoord = (Vector2)propertyInfo.GetValue(targetModel);
        texcoord[channel] = val;
        propertyInfo.SetValue(targetModel, texcoord);
    }
}
