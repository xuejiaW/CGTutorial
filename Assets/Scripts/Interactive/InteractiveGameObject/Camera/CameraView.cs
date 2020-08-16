using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : InteractiveGameObjectView
{
    public new CameraModel model = null;
    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as CameraModel;
        this.model.lineRenders = GetComponentsInChildren<LineRenderer>().ToList();
        this.model.camera = GetComponent<Camera>();
    }
}
