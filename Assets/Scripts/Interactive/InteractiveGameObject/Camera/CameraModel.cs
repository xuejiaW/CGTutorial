using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModel : InteractiveGameObjectModel
{
    public override System.Type GetViewType() { return typeof(CameraView); }
    public override System.Type GetControllerType() { return typeof(CameraController); }

    public List<LineRenderer> lineRenders = null;
    public Camera camera = null;

    public event System.Action<Vector3> OnCameraPropertyUpdated = null;
    public Vector3 cameraProperty
    {
        get { return new Vector3(camera.fieldOfView, camera.nearClipPlane, camera.farClipPlane); }
        set
        {
            camera.fieldOfView = value[0];
            camera.nearClipPlane = value[1];
            camera.farClipPlane = value[2];
            OnCameraPropertyUpdated?.Invoke(value);
        }
    }
}
