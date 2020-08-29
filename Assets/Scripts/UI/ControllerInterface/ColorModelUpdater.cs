using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorModelUpdater : IUpdateModelProperty
{
    private string property = "color";
    public void UpdateModelProperty(EntityModel target, int channel, string value)
    {
        PropertyInfo info = target.GetType().GetProperty(property);
        Debug.Assert(info != null, "Property: " + property + " in UpdateModelProperty is null");

        float.TryParse(value, out float val);

        Color color = (Color)info.GetValue(target);
        color[channel] = val;
        info.SetValue(target, color);
    }
}
