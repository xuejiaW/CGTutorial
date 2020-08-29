using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorUIController : ComponentController
{
    public override IUpdateModelProperty GetModelUpdater()
    {
        return new ColorModelUpdater();
    }
}
