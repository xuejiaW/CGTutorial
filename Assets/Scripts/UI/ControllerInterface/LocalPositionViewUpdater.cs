using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalPositionViewUpdater : UpdateViewBase
{
    public override UpdateViewBase SetTargetModel(EntityModel model)
    {
        base.SetTargetModel(model);
        targetModelUpdatedEvent = targetModel.GetType().GetEvent("OnLocalPositionUpdated");
        viewUpdateMethod = GetType().GetMethod("UpdateLocalPosition", BindingFlags.NonPublic | BindingFlags.Instance);
        handler = Delegate.CreateDelegate(targetModelUpdatedEvent.EventHandlerType, this, viewUpdateMethod);
        return this;
    }

    private void UpdateLocalPosition(Vector3 localPos)
    {
        targets[0].text = (localPos[0]).ToString("f2");
        targets[1].text = (localPos[1]).ToString("f2");
        targets[2].text = (-localPos[2]).ToString("f2");
    }

    public override void UpdateView(object data)
    {
        Vector3 localPos = (Vector3)data;
        UpdateLocalPosition(localPos);
    }
}
