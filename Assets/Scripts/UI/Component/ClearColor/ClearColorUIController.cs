using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorUIController : ComponentController
{
    public override UpdateModelPropertyBase GetModelUpdater()
    {
        return new ColorModelUpdater();
    }
}
