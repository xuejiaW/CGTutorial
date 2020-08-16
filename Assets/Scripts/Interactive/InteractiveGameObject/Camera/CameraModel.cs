using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModel : InteractiveGameObjectModel
{
    public override System.Type GetViewType() { return typeof(CameraView); }
    public override System.Type GetControllerType() { return typeof(CameraController); }

    public List<LineRenderer> lineRenders = null;
    public Camera camera = null;

    public event System.Action<float> OnFovUpdated = null;
    public event System.Action<float> OnNearClippingUpdated = null;
    public event System.Action<float> OnFarClippingUpdated = null;

    public float nearClipping
    {
        get { return camera.nearClipPlane; }
        set
        {
            camera.nearClipPlane = value;
            OnNearClippingUpdated?.Invoke(value);
        }
    }

    public float farClipPlane
    {
        get { return camera.farClipPlane; }
        set
        {
            camera.farClipPlane = value;
            OnFarClippingUpdated?.Invoke(value);
        }
    }

    public float fov
    {
        get { return camera.fieldOfView; }
        set
        {
            camera.fieldOfView = value;
            OnFovUpdated?.Invoke(value);
        }
    }
}
