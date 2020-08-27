using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorModelUpdater : IUpdateModelProperty
{
    public void UpdateModelProperty(EntityModel target, int channel, string value)
    {
        ClearColorModel model = target as ClearColorModel;

        float.TryParse(value, out float val);

        Color backgroundColor = model.clearColor;
        backgroundColor[channel] = val;
        model.clearColor = backgroundColor;
    }
}
