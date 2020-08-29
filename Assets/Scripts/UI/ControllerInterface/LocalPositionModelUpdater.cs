using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPositionModelUpdater : IUpdateModelProperty
{
    private string property = "localPosition";
    public void UpdateModelProperty(EntityModel target, int channel, string value)
    {
        PropertyInfo info = target.GetType().GetProperty(property);
        Debug.Assert(info != null, "Property: " + property + " in UpdateModelProperty is null");

        float.TryParse(value, out float val);

        Vector3 localPos = (Vector3)info.GetValue(target);
        localPos[channel] = val;
        info.SetValue(target, localPos);
    }
}
