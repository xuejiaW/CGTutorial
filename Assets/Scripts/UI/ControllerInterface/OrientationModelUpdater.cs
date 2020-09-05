using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationModelUpdater : UpdateModelPropertyBase
{
    public override void SetTargetModel(EntityModel model)
    {
        base.SetTargetModel(model);
        propertyInfo = targetModel.GetType().GetProperty("localRotation");
    }

    public override void UpdateModelProperty(int channel, string value)
    {
        float.TryParse(value, out float val);

        if (channel == 0)
            val *= -1;
        if (channel == 1)
            val = 270 - val;
        else if (channel == 2)
            val -= 90;

        Vector3 localRot = ((Quaternion)propertyInfo.GetValue(targetModel)).eulerAngles;
        localRot[channel] = val;
        propertyInfo.SetValue(targetModel, Quaternion.Euler(localRot));
    }
}
