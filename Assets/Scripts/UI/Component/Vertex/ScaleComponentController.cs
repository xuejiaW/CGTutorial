using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleComponentController : TransformComponentController
{
    public override UpdateModelPropertyBase GetModelUpdater()
    {
        return new LocalScaleModelUpdater();
    }
}
