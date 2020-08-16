using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : InteractiveGameObjectController
{
    public new CameraModel model = null;
    private float cameraNear2FarDistance = 0.0f;

    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as CameraModel;
    }

    public override void Init()
    {
        base.Init();
        cameraNear2FarDistance = model.farClipPlane - model.nearClipping;

        model.lineRenders.ForEach(lineRender =>
        {
            lineRender.startWidth = 0.1f;
            lineRender.endWidth = 0.1f;
        });

        model.OnFovUpdated += (fov => UpdateCameraLine());
        model.OnNearClippingUpdated += (near => UpdateCameraLine());
        model.OnFarClippingUpdated += (far => UpdateCameraLine());
        model.OnPositionUpdated += (pos => UpdateCameraLine());
        model.OnRotationUpdated += (rot => UpdateCameraLine());
        model.OnParentUpdated += (parent => UpdateCameraLine());

        UpdateCameraLine();
    }

    private void UpdateCameraLine()
    {
        cameraNear2FarDistance = model.farClipPlane - model.nearClipping;
        Vector3[] viewpoint = new Vector3[]
        {
            new Vector3(0,0,1),
            new Vector3(1,0,1),
            new Vector3(1,1,1),
            new Vector3(0,1,1)
        };

        for (int i = 0; i != 4; ++i)
        {
            Ray ray = model.camera.ViewportPointToRay(viewpoint[i]);
            model.lineRenders[i].SetPosition(0, ray.GetPoint(0));
            model.lineRenders[i].SetPosition(1, ray.GetPoint(cameraNear2FarDistance));
            model.lineRenders[4].SetPosition(i, ray.GetPoint(0));
            model.lineRenders[5].SetPosition(i, ray.GetPoint(cameraNear2FarDistance));
        }

    }
}
