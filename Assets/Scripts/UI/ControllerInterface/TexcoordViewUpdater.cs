using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexcoordViewUpdater : UpdateViewBase
{
    public override UpdateViewBase SetTargetModel(EntityModel model)
    {
        base.SetTargetModel(model);
        targetModelUpdatedEvent = targetModel.GetType().GetEvent("OnTexcoordUpdated");
        viewUpdateMethod = GetType().GetMethod("UpdateTexcoord", BindingFlags.NonPublic | BindingFlags.Instance);
        handler = Delegate.CreateDelegate(targetModelUpdatedEvent.EventHandlerType, this, viewUpdateMethod);
        return this;
    }

    private void UpdateTexcoord(Vector2 texcoord)
    {
        for (int i = 0; i != targets.Count; ++i)
        {
            targets[i].text = (texcoord[i]).ToString("f2");
        }
    }

    public override void UpdateView(object data)
    {
        Vector2 texcoord = (Vector2)data;
        UpdateTexcoord(texcoord);
    }
}
