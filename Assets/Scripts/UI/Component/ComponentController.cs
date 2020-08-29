using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentController : DisplayableEntityController
{
    public new ComponentModel model = null;

    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as ComponentModel;
    }

    public virtual void InitComponent() { }

    private IUpdateModelProperty _modelUpdater;
    public IUpdateModelProperty modelUpdater
    {
        get
        {
            if (_modelUpdater == null)
                _modelUpdater = GetModelUpdater();
            return _modelUpdater;
        }
    }
    public virtual IUpdateModelProperty GetModelUpdater() { return null; }

    public virtual void UpdateModelProperty(int channel, string val)
    {
        modelUpdater.UpdateModelProperty(model.targetGameObject, channel, val);
    }
}
