using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpdateModelProperty
{
    void UpdateModelProperty(EntityModel target, int channel, string value);
}
