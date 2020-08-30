using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalRotationViewUpdater : UpdateViewBase
{
    public override UpdateViewBase SetTargetModel(EntityModel model)
    {
        base.SetTargetModel(model);
        targetModelUpdatedEvent = targetModel.GetType().GetEvent("OnLocalRotationUpdated");
        viewUpdateMethod = GetType().GetMethod("UpdateLocalRotation", BindingFlags.NonPublic | BindingFlags.Instance);
        handler = Delegate.CreateDelegate(targetModelUpdatedEvent.EventHandlerType, this, viewUpdateMethod);
        return this;
    }

    private void UpdateLocalRotation(Quaternion localRot)
    {
        Vector3 euler = localRot.eulerAngles;
        for (int i = 0; i != 3; ++i)
        {
            targets[i].text = (rotateValueClamp(euler[i]) * -1.0f).ToString("f2");
        }
    }

    public override void UpdateView(object data)
    {
        UpdateLocalRotation((Quaternion)data);
    }

    private float rotateValueClamp(float value)
    {
        float result = value - 360.0f * (((int)value + 180) / 360);
        return result;
    }
}
