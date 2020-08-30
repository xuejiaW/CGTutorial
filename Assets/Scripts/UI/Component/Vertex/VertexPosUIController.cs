using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexPosUIController : TransformComponentController
{
    public override UpdateModelPropertyBase GetModelUpdater()
    {
        return new LocalPositionModelUpdater();
    }
}
