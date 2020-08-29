using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalPositionComponentUpdater : IUpdateView
{
    public List<InputField> targets = null;

    public void SetTargetInputFields(List<InputField> targets)
    {
        this.targets = targets;
    }

    public void SetTargetView(object model)
    {
        throw new NotImplementedException();
    }

    public void UpdateComponent(object data)
    {
        Vector3? localPos = data as Vector3?;
        for (int i = 0; i != 3; ++i)
        {
            targets[i].text = (localPos.Value[i]).ToString();
        }
    }
}
