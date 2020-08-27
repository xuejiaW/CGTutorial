using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorComponentUpdater : IUpdateComponent<Color>
{
    public List<InputField> targets = null;
    public void SetTargetInputFields(List<InputField> targets)
    {
        this.targets = targets;
    }

    public void UpdateComponent(IEquatable<Color> data)
    {
        Color? color = data as Color?;
        for (int i = 0; i != 3; ++i)
        {
            targets[i].text = (color.Value[i]).ToString();
        }
    }
}
