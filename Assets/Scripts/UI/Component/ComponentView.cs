using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentView : EntityView
{
    public InputField[] inputFields = null;
    public new ComponentModel model = null;

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as ComponentModel;
        inputFields = GetComponentsInChildren<InputField>();
    }

    public virtual void InitComponent() { }
}
