using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorViewUpdater : IUpdateView
{
    public List<InputField> targets = null;

    public void SetTargetView(object view)
    {
        // Dynamic get inputFields
        FieldInfo fieldInfo = view.GetType().GetField("inputFields");
        Debug.Assert(fieldInfo != null, "Can not find inputsField");
        targets = (List<InputField>)fieldInfo.GetValue(view);


        EntityModel model = view.GetType().GetField("model").GetValue(view) as EntityModel;
        // Dynamic set event to the targetGameObject.OnColorChanged
        EntityModel targetGO = model.GetType().GetProperty("targetGameObject").GetValue(model) as EntityModel;
        EventInfo eventInfo = targetGO.GetType().GetEvent("OnColorChanged");
        Debug.Assert(eventInfo != null, "Get not find event");
        MethodInfo methodInfo = GetType().GetMethod("UpdateColor");
        Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, methodInfo);
        eventInfo.AddEventHandler(targetGO, handler);
    }

    public void UpdateColor(Color color)
    {
        for (int i = 0; i != 3; ++i)
        {
            targets[i].text = (color[i]).ToString();
        }
    }

}
