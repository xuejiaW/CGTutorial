using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewUpdater : UpdateViewBase
{
    public override UpdateViewBase SetTargetModel(EntityModel model)
    {
        base.SetTargetModel(model);
        targetModelUpdatedEvent = targetModel.GetType().GetEvent("OnCameraPropertyUpdated");
        viewUpdateMethod = GetType().GetMethod("UpdateCameraProperty", BindingFlags.NonPublic | BindingFlags.Instance);
        handler = Delegate.CreateDelegate(targetModelUpdatedEvent.EventHandlerType, this, viewUpdateMethod);
        propertyInfo = targetModel.GetType().GetProperty("cameraProperty");
        return this;
    }

    private void UpdateCameraProperty(Vector3 cameraProperty)
    {
        targets[0].text = cameraProperty[0].ToString("f2");
        targets[1].text = cameraProperty[1].ToString("f2");
        targets[2].text = cameraProperty[2].ToString("f2");
    }

    public override void UpdateView(object data)
    {
        UpdateCameraProperty((Vector3)data);
    }

    public override void UpdateView()
    {
        Vector3 cameraProperty = (Vector3)propertyInfo.GetValue(targetModel);
        UpdateCameraProperty(cameraProperty);
    }
}
