using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorComponentController : ComponentController
{
    public override UpdateModelPropertyBase GetModelUpdater()
    {
        return new ColorModelUpdater();
    }
}
