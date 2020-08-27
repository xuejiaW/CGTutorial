using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentView : EntityView
{
    public List<InputField> inputFields = null;

    public new ComponentModel model = null;
    public new ComponentController controller = null;

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as ComponentModel;
        inputFields = GetComponentsInChildren<InputField>().ToList();
    }

    public override void BindEntityController(DisplayableEntityController controller)
    {
        base.BindEntityController(controller);
        this.controller = base.controller as ComponentController;
    }

    public virtual void InitComponent() { }
}
