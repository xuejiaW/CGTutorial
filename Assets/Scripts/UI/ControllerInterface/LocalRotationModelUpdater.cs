using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalRotationModelUpdater : UpdateModelPropertyBase
{
    public override void SetTargetModel(EntityModel model)
    {
        base.SetTargetModel(model);
        propertyInfo = targetModel.GetType().GetProperty("localRotation");
    }

    public override void UpdateModelProperty(int channel, string value)
    {
        float.TryParse(value, out float val);
        // val *= -1;
        Vector3 localRot = ((Quaternion)propertyInfo.GetValue(targetModel)).eulerAngles;
        // if (channel == 1 || channel == 2)
        //     val *= -1;
        localRot[channel] = val;
        propertyInfo.SetValue(targetModel, Quaternion.Euler(localRot));
    }
}
