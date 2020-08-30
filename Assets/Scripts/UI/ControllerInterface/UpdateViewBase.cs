using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UpdateViewBase : IUpdateView
{
    protected List<InputField> targets = null;
    // protected EntityModel model = null;
    protected EntityModel targetModel = null;
    protected EventInfo targetModelUpdatedEvent = null;
    protected MethodInfo viewUpdateMethod = null;
    protected PropertyInfo propertyInfo = null;
    protected Delegate handler = null;

    public virtual UpdateViewBase SetTargetView(object view) // the view may not inherits from EntityView
    {
        FieldInfo fieldInfo = view.GetType().GetField("inputFields");
        Debug.Assert(fieldInfo != null, "Can not find inputsField");
        targets = (List<InputField>)fieldInfo.GetValue(view);
        return this;
    }

    public virtual UpdateViewBase SetTargetModel(EntityModel model)
    {
        targetModel = model;
        return this;
    }

    public virtual void RegisterEvent()
    {
        targetModelUpdatedEvent?.AddEventHandler(targetModel, handler);
    }

    public virtual void UnRegisterEvent()
    {
        targetModelUpdatedEvent?.RemoveEventHandler(targetModel, handler);
    }

    public virtual void UpdateView(object data) { }

    public virtual void UpdateView() { }
}
