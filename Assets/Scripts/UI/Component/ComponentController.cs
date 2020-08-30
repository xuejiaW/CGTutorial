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

    public virtual void InitComponent()
    {
        modelUpdater?.SetTargetModel(model.targetGameObject);
    }

    private UpdateModelPropertyBase _modelUpdater;
    public UpdateModelPropertyBase modelUpdater
    {
        get
        {
            if (_modelUpdater == null)
                _modelUpdater = GetModelUpdater();
            return _modelUpdater;
        }
    }
    public virtual UpdateModelPropertyBase GetModelUpdater() { return null; }

    public virtual void UpdateModelProperty(int channel, string val)
    {
        modelUpdater?.UpdateModelProperty(channel, val);
    }
}
