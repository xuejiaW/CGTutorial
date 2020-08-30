﻿using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorViewUpdater : UpdateViewBase
{
    public override UpdateViewBase SetTargetModel(EntityModel model)
    {
        base.SetTargetModel(model);
        targetModelUpdatedEvent = targetModel.GetType().GetEvent("OnColorUpdated");
        viewUpdateMethod = GetType().GetMethod("UpdateColor", BindingFlags.NonPublic | BindingFlags.Instance);
        handler = Delegate.CreateDelegate(targetModelUpdatedEvent.EventHandlerType, this, viewUpdateMethod);
        return this;
    }

    private void UpdateColor(Color color)
    {
        for (int i = 0; i != 3; ++i)
        {
            targets[i].text = (color[i]).ToString();
        }
    }

    public override void UpdateView(object data)
    {
        Color color = (Color)data;
        UpdateColor(color);
    }
}
