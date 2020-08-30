using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateModelPropertyBase : IUpdateModelProperty
{
    protected EntityModel targetModel = null;
    protected PropertyInfo propertyInfo = null;
    public virtual void SetTargetModel(EntityModel model)
    {
        targetModel = model;
    }

    public virtual void UpdateModelProperty(int channel, string value)
    {
        float.TryParse(value, out float val);
        throw new System.NotImplementedException();
    }
}
