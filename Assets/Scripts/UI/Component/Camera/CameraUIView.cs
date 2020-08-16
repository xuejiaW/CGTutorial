using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraUIView : EntityView
{
    private new CameraUIModel model = null;

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);
        Debug.Log("enter bind view");
        this.model = base.model as CameraUIModel;
        this.model.inputFields = GetComponentsInChildren<InputField>();
        if (this.model.inputFields == null)
            Debug.Log("input null in view");
        else
            Debug.Log("input not null in view");
    }
}
