using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModelUpdater : UpdateModelPropertyBase
{
    public override void SetTargetModel(EntityModel model)
    {
        base.SetTargetModel(model);
        propertyInfo = targetModel.GetType().GetProperty("cameraProperty");
    }

    public override void UpdateModelProperty(int channel, string value)
    {
        float.TryParse(value, out float val);

        Vector3 cameraProperty = ((Vector3)propertyInfo.GetValue(targetModel));
        cameraProperty[channel] = val;
        propertyInfo.SetValue(targetModel, cameraProperty);
    }
}
