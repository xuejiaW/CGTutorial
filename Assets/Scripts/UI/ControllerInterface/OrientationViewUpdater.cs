using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationViewUpdater : UpdateViewBase
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

        targets[0].text = (rotateValueClamp(euler[0]) * -1.0f).ToString("f2");
        targets[1].text = (rotateValueClamp(euler[1] - 90.0f)).ToString("f2");
        targets[2].text = (rotateValueClamp(90 - euler[2])).ToString("f2");
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
