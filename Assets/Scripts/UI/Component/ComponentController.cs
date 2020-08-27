using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentController : DisplayableEntityController
{
    public new ComponentModel model = null;

    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as ComponentModel;
    }

    public virtual void InitComponent() { }
}
