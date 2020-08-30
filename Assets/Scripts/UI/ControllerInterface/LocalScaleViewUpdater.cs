using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalScaleViewUpdater : UpdateViewBase
{
    public override UpdateViewBase SetTargetModel(EntityModel model)
    {
        base.SetTargetModel(model);
        targetModelUpdatedEvent = targetModel.GetType().GetEvent("OnLocalScaleUpdated");
        viewUpdateMethod = GetType().GetMethod("UpdateLocalScale", BindingFlags.NonPublic | BindingFlags.Instance);
        handler = Delegate.CreateDelegate(targetModelUpdatedEvent.EventHandlerType, this, viewUpdateMethod);
        return this;
    }

    private void UpdateLocalScale(Vector3 scale)
    {
        Vector3 holdingGOScale = InteractiveGameObjectCollection.Instance.holdingInteractiveGo != null
                         ? InteractiveGameObjectCollection.Instance.holdingInteractiveGo.localScale : Vector3.one;

        scale = scale.Times(holdingGOScale);
        for (int i = 0; i != 3; ++i)
        {
            targets[i].text = scale[i].ToString("f2");
        }
    }

    public override void UpdateView(object data)
    {
        UpdateLocalScale((Vector3)data);
    }
}
